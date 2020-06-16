using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Wegplanung
{
   /// <summary>
   /// Klasse zur Festlegung von Programmweiten Parametern
   /// </summary>
   /// <remarks>
   /// Historie:  23.05.2012  (awi) Erstellt
   /// </remarks>
   class Konstante
   {
      /// <summary>
      /// Unendlich (Kosten, Distanz, etc.)
      /// </summary>
      public const int UNENDLICH = -1;

      /// <summary>
      /// Markierung kein Vorgänger Knoten
      /// </summary>
      public const int KEIN_VORGAENGER = -1;

      /// <summary>
      /// Maximale Anzahl Zeilen
      /// </summary>
      public const int MAX_ZEILE = 20;

      /// <summary>
      /// Maximale Anzahl Spalten
      /// </summary>
      public const int MAX_SPALTE = 20;
   }

   /// <summary>
   /// Klasse zur Steuerung des Programms und zur Berechnung der Ergebnisse
   /// </summary>
   /// <remarks>
   /// Historie:  22.05.2012  (awi) Erstellt
   /// </remarks>
   class Program
   {
      /// <summary>
      /// Die Main-Methode, steuert den Programmablauf
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="args">Die Programmparameter. Es kann ein Dateiname zum Einlesen angegeben werden</param>
      static void Main(string[] args)
      {
         // Zeitmessung zur Performance-Verbesserung
         Stopwatch watch = new Stopwatch();
         watch.Start();

         // Initialisierung
         Ergebnis erg = new Ergebnis();
         Ausgabe ausg = new Ausgabe();
         Eingabe eing = new Eingabe();

         String sDateinameEingabe = "";
         String sDateinameAusgabe = "";

         if (args.Length > 0)
         {
            // Parameter zum einlesen der Datei
            sDateinameEingabe = args[0];
         }
         else
         {
            // Standard Input verwenden
            sDateinameEingabe = @"Testfaelle/gebiet.txt";
         }

         // Ausgabe-Dateinamen festlegen (NameEingabedatei_Ergebnis.txt)
         sDateinameAusgabe = Path.GetDirectoryName(sDateinameEingabe) 
            + @"\" + Path.GetFileNameWithoutExtension(sDateinameEingabe) + "_Ergebnis.txt";

         try
         {
            // Datei einlesen
            eing.DateiEinlesen(sDateinameEingabe, erg);
         }
         catch(Exception e)
         {
            ausg.FehlerAusgabe("Fehler beim Einlesen der Datei: " + e.Message, sDateinameAusgabe);
            return;
         }

         // Feld in Adjazenz Matrix umwandeln
         eing.FeldUmwandeln(erg);

         // Kosten abschätzen
         KostenAbsch(erg);

         // Minimale Kosten + Weg bestimmen
         KostenMinimal(erg);

         watch.Stop();
         ausg.StringAusgabe("Ausführungszeit: " + watch.Elapsed);

         // Ergebnis ausgeben
         ausg.ErgebnisAusgabe(erg);

         // Ergebnis in Datei ausgeben
         ausg.ErgebnisDateiAusgabe(sDateinameAusgabe, erg);

         // Programm beenden
         // Console.ReadKey();
      }

      /// <summary>
      /// Errechnet eine Kostenabschätzung
      /// </summary>
      /// <remarks>
      /// Hinweise:  -  errechnet eine triviale Lösung des Problems. 
      ///               Läuft im eingelesenen Feld zunächst vom Start auf 
      ///               die Höhe des Ziels und von dort zum Ziel. 
      ///               Die summierten Kosten dieses Weges ist die 
      ///               Kostenabschätzung
      ///            -  Feld im Ergebnis Objekt muss bereits angelegt sein
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="e">Ein Ergebnis Objekt</param>
      static void KostenAbsch(Ergebnis e)
      {
         int iKosten = 0;
         int iIterator;

         if (e.Feld == null)
         {
            throw new Exception("Feld im Ergebnis Objekt nicht angelegt");
         }

         //vertikale Richtung 
         if (e.si < e.zi)
         {
            // Ziel liegt "unterhalb" von Start
            iIterator = 1;
         }
         else
         {
            // Ziel liegt "oberhalb" von Start
            iIterator = -1;
         }

         // Weg gehen und Kosten summieren
         for (int i = e.si; i != e.zi; i = i + iIterator)
         {
            iKosten += e.Feld[i,e.sk];
         }

         //horizontale Richtung 
         if (e.sk < e.zk)
         {
            // Ziel liegt "rechts" von Start
            iIterator = 1;
         }
         else
         {
            // Ziel liegt "links" von Start
            iIterator = -1;
         }

         // Weg gehen und Kosten summieren
         for (int k = e.sk; k != e.zk; k = k + iIterator)
         {
            iKosten += e.Feld[e.zi, k];
         }

         //Kosten speichern
         e.iKostenAbsch = iKosten;
      }

      /// <summary>
      /// Errechnet einen optimalen Weg mit Kostenminimum
      /// </summary>
      /// <remarks>
      /// Hinweise:  -  errechnet eine optimale Lösung des Problems inklusive dem Weg. 
      ///               Es wird ein angepasster Dijkstra-Algorithmus verwendet
      ///            -  die Adjazenzmatrix des Ergebnis Objekts muss bereits erstellt 
      ///               sein
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="e">Ein Ergebnis Objekt</param>
      static void KostenMinimal(Ergebnis e)
      {
         if (e.AdjazenzMatrix == null)
         {
            throw new Exception("Adjazenzmatrix im Ergebnis Objekt muss angelegt sein");
         }

         int iMinimum;
         int iKnoten;

         //Initialisierung
         int iAnzahlKnoten = e.AdjazenzMatrix.GetLength(0);

         int[] kosten = new int[iAnzahlKnoten];             // Array für Kosten
         int[] distanz = new int[iAnzahlKnoten];            // Array für Entfernung
         int[] vorgaenger = new int[iAnzahlKnoten];         // Array für Vorgängerknoten

         int iKostenNeu, iDistanzNeu;

         bool[] markiert = new bool[iAnzahlKnoten];

         for (int i = 0; i < iAnzahlKnoten; ++i)
         {
            // kein Knoten markiert
            markiert[i] = false;

            // Kosten auf unendlich setzen
            kosten[i] = Konstante.UNENDLICH;

            // Distanz auf unendlich setzen
            distanz[i] = Konstante.UNENDLICH;

            // keine Vorgänger
            vorgaenger[i] = Konstante.KEIN_VORGAENGER;
         }

         // Startknoten mit Kosten/Distanz 0
         kosten[e.iStartknoten] = 0;
         distanz[e.iStartknoten] = 0;

         bool bWeitereKnoten = true;

         while (bWeitereKnoten)
         {
            // minimale Kosten initialisieren
            iMinimum = Int32.MaxValue;

            // zugehöriger (minimaler) Knoten
            iKnoten = 0;

            // Minimale Distanz ermitteln
            for (int j = 0; j < iAnzahlKnoten; j++)
            {
               // Knoten schon markiert -> überspringen (Vermeidung von Zyklen!)
               if (markiert[j])
               {
                  continue;
               }

               // Distanz kleiner als Minimum -> neues Minimum gefunden
               if (kosten[j] != Konstante.UNENDLICH && kosten[j] < iMinimum)
               {
                  iMinimum = kosten[j];
                  iKnoten = j;
               }
            }

            // Distanz aktualisieren, wenn Zielknoten über den gefundenen
            // Minimumknoten billiger erreichbar
            for (int j = 0; j < iAnzahlKnoten; j++)
            {
               // wenn nicht markiert und nicht Verbindung gleicher Knoten und Verbindung vorhanden
               if (  !markiert[j] && 
                     iKnoten != j && 
                     e.AdjazenzMatrix[iKnoten, j] != Konstante.UNENDLICH)
               {
                  // Neuberechnung von Distanz und Kosten
                  iKostenNeu = kosten[iKnoten] + e.AdjazenzMatrix[iKnoten, j];
                  iDistanzNeu = distanz[iKnoten] + 1;

                  // besteht Verbesserung? Unendlich ist immer zu verbessern, 
                  // ansonsten vergleichen, ob neuer Wert besser als alter Wert
                  // oder, Kosten gleich, kürzeren Weg wählen
                  if (kosten[j] == Konstante.UNENDLICH || 
                     kosten[j] > iKostenNeu ||
                     (kosten[j] == iKostenNeu && distanz[j] > iDistanzNeu))
                  {
                     // Verbesserung gefunden
                     kosten[j] = iKostenNeu;
                     distanz[j] = iDistanzNeu; 
                     vorgaenger[j] = iKnoten;
                  }
               }
            }

            // gerade bestimmten Minimumknoten markieren
            markiert[iKnoten] = true;

            // sind noch Knoten nicht markiert -> weiter, sonst Ende
            bWeitereKnoten = false;
            for (int j = 0; j < iAnzahlKnoten && !bWeitereKnoten; j++)
            {
               bWeitereKnoten = !markiert[j];
            }
         }
         
         // kuerzester Weg speichern

         // Länge des kürzesten Weges
         e.kuerzesterWeg = new int[distanz[e.iZielknoten] + 1];   
         
         // Vorgänger verfolgen bis zum Ziel ("Rückwärtssuche")

         // Rückwärtssuche startet beim Zielknoten
         iKnoten = e.iZielknoten;

         // Ergebnis Array von hinten befüllen     
         int kuerzesterWegIterator = distanz[e.iZielknoten];      
         while (vorgaenger[iKnoten] != Konstante.KEIN_VORGAENGER)
         {
            // Knoten speichern
            e.kuerzesterWeg[kuerzesterWegIterator] = iKnoten;
            // Vorgänger festlegen
            iKnoten = vorgaenger[iKnoten];                        
            kuerzesterWegIterator--;                           
         }

         // Startknoten speichern
         e.kuerzesterWeg[0] = e.iStartknoten;

         // Minimalkosten speichern
         e.iKostenMinimal = kosten[e.iZielknoten];                
      }
   }
}
