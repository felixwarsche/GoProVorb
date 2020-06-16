using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wegplanung
{
   /// <summary>
   /// Klasse zur Ausgabe von Ergebnissen, Fehlern und Informationen. Außerdem können Datenstrukturen zu Testzwecken ausgegeben werden.
   /// </summary>
   /// <remarks>
   /// Historie:  22.05.2012  (awi) Erstellt
   /// </remarks>
   class Ausgabe
   {

      /// <summary>
      /// Gibt das Ergebnis in der vorgegebenen Form auf dem Bildschirm aus
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      ///            23.05.2012  (awi) Ausgabe in gesonderte Methode gekapselt
      /// </remarks>
      /// <param name="e">Ein Ergebnis Objekt</param>
      public void ErgebnisAusgabe(Ergebnis e)
      {
         // Console als SW übergeben
         StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding);
         sw.AutoFlush = true;
         ErgebnisAusgabe(sw, e);
         sw.Close();
      }
      
      /// <summary>
      /// Gibt das Ergebnis in der vorgegebenen Form in einer Datei aus
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      ///            23.05.2012  (awi) Ausgabe in gesonderte Methode gekapselt
      /// </remarks>
      /// <param name="sDateiname">Ein String mit einem Dateinamen</param>
      /// <param name="e">Ein Ergebnis Objekt</param>
      public void ErgebnisDateiAusgabe(String sDateiname, Ergebnis e)
      {
         // Datei als SW öffnen
         StreamWriter sw;
         try
         {
            sw = new StreamWriter(sDateiname, false, Encoding.UTF8);
         }
         catch
         {
            throw new FieldAccessException("Datei (" + sDateiname + ") konnte nicht zum Schreiben geöffnet werden.");
         }

         try
         {
            ErgebnisAusgabe(sw, e);
         }
         catch
         {
            Console.WriteLine("Ergebnis konnte nicht in Datei (" + sDateiname + ") geschrieben werden.");
         }

         sw.Close();
      }

      /// <summary>
      /// Gibt das Ergebnis in der vorgegebenen Form in einem StreamWriter aus
      /// </summary>
      /// <remarks>
      /// Historie:  23.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="sw">Ein StreamWriter zur Ausgabe</param>
      /// <param name="e">Ein Ergebnis Objekt</param>
      private void ErgebnisAusgabe(StreamWriter sw, Ergebnis e)
      {
         if (sw != null)
         {
            sw.WriteLine(e.sKommentar);
            sw.WriteLine("Startzelle: {0}, {1}, Zielzelle: {2}, {3}", e.si + 1, e.sk + 1, e.zi + 1, e.zk + 1);
            sw.WriteLine("Abschätzung der Kostenobergrenze: {0} KE", e.iKostenAbsch);
            sw.WriteLine("Minimalkosten: {0} KE", e.iKostenMinimal);
            sw.Write("Weg: ");
            for (int iKnoten = 0; iKnoten < e.kuerzesterWeg.GetLength(0); iKnoten++)
            {
               if (iKnoten == 0)
               {
                  sw.Write("S; ");
               }
               else if (iKnoten == e.kuerzesterWeg.GetLength(0) - 1)
               {
                  sw.Write("Z\n");
               }
               else
               {
                  sw.Write("{0},", e.ZeileZuKnoten(e.kuerzesterWeg[iKnoten]) + 1);
                  sw.Write("{0}; ", e.SpalteZuKnoten(e.kuerzesterWeg[iKnoten]) + 1);
               }
            }
         }
      }

      /// <summary>
      /// Gibt einen Fehler auf Bildschirm und in einer Datei aus
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="sFehler">Ein String mit einem Fehlertext</param>
      /// <param name="sDateiname">Ein String mit dem Dateinamen zur Fehlerausgabe</param>
      public void FehlerAusgabe(String sFehler, String sDateiname)
      {
         Console.WriteLine(sFehler);

         // Fehler in Ausgabedatei schreiben
         StreamWriter sr;
         try
         {
            sr = new StreamWriter(sDateiname, false, Encoding.UTF8);
         }
         catch
         {
            Console.WriteLine("Datei (" + sDateiname + ") konnte nicht zum Schreiben geöffnet werden");
            return;
         }

         sr.WriteLine(sFehler);

         sr.Close();
      }

      /// <summary>
      /// Gibt einen String auf dem Bildschirm aus
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="sString">Ein String mit einem Infotext</param>
      public void StringAusgabe(String sString)
      {
         Console.WriteLine(sString);
      }

      /// <summary>
      /// Gibt eine Adjazenzmatrix des Ergebnis Objekts auf dem Bildschirm zu Testzwecken aus
      /// Hinweise:  Unendlich wird als INF (infinity) dargestellt
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </summary>
      /// <param name="e">Ein Ergebnis Objekt</param>
      public void AdjazenzMatrixAusgabe(Ergebnis e)
      {
         String s;

         // über alle Zeilen
         for (int i = 0; i < e.AdjazenzMatrix.GetLength(0); i++)
         {
            s = "";  // String für die Zeilenweise Ausgabe
            // über alle Spalten
            for (int k = 0; k < e.AdjazenzMatrix.GetLength(1); k++)
            {
               if (e.AdjazenzMatrix[i, k] == Konstante.UNENDLICH)
               {
                  // Unendlich wird als INF (infinity) ausgegeben
                  s = s + "INF , ";
               }
               else
               {
                  // Wert ausgeben
                  s = s + String.Format("{0,3}", e.AdjazenzMatrix[i, k]) + " , ";
               }
            }
            Console.WriteLine(s);
         }
      }

      /// <summary>
      /// Gibt ein Feld des Ergebnis Objekts auf dem Bildschirm zu Testzwecken aus
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="e">Ein Ergebnis Objekt</param>
      public void FeldAusgabe(Ergebnis e)
      {
         String s;

         // über alle Zeilen
         for (int i = 0; i < e.Feld.GetLength(0); i++)
         {
            s = ""; // String zur zeilenweisen Ausgabe
            // über alle Spalten
            for (int k = 0; k < e.Feld.GetLength(1); k++)
            {
               s = s + e.Feld[i, k] + " , ";
            }
            Console.WriteLine(s);
         }
      }

   }
}
