using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wegplanung
{
   /// <summary>
   /// Klasse zur Eingabe und Umwandlung von Daten.
   /// </summary>
   /// <remarks>
   /// Historie:  22.05.2012  (awi) Erstellt
   /// </remarks>
   class Eingabe
   {

      /// <summary>
      /// Liest eine Datei ein ein und speichert in Ergebnis Objekt
      /// </summary>
      /// <remarks>
      /// Hinweise:  Löst verschiedene Ausnahmen aus, wenn Datei nicht geladen werden kann oder Datei 
      ///            fehlerhaft
      ///            S, Z werden als 0 im Feld markiert
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="sDateiname">Ein String mit einem Dateinamen</param>
      /// <param name="e">Ein Ergebnis Objekt</param>
      public void DateiEinlesen(String sDateiname, Ergebnis e)
      {
         bool bStartgefunden = false;
         bool bZielgefunden = false;
         int iSpalten = 0;
         int iZeile = 0;
         String sTmp, sZelle, sTmpAusn;
         String[] sTmpArr;
         int iTmp;

         StreamReader sr;

         int[,] tmpFeld = new int[Konstante.MAX_ZEILE, Konstante.MAX_SPALTE];

         if (File.Exists(sDateiname))
         {
            // Einlesen mit ISO-8859-1 (Westeuropäisch), wird wegen Umlauten in Kommentar verwendet
            sr = new StreamReader(File.Open(sDateiname, FileMode.Open), Encoding.GetEncoding(28591));
         }
         else
         {
            throw new FileNotFoundException("Die angegebene Datei wurde nicht gefunden");
         }

         if (sr.Peek() == -1)
         {
            throw new FileLoadException("Die angegebene Datei ist leer");
         }

         // erste Zeile lesen, erwartet Kommentar
         sTmp = sr.ReadLine();
         if (sTmp.StartsWith(";"))
         {
            // Kommentar in Ergebnis Objekt speichern
            e.sKommentar = sTmp.Substring(1).Trim();
         }
         else
         {
            throw new FileLoadException("Die angegebene Datei erhält eine fehlerhafte 1. Zeile");
         }

         // weitere Zeilen durchlaufen
         while (sr.Peek() >= 0)
         {
            if (iZeile >= Konstante.MAX_ZEILE )
            {
               sTmpAusn = String.Format("Das Feld darf nicht mehr als {0} Zeilen haben.", 
                  Konstante.MAX_ZEILE);
               throw new FileLoadException(sTmpAusn);
            }

            sTmp = sr.ReadLine();
            if(sTmp.StartsWith(";"))
            {
               // Kommentar gefunden
               continue;
            }

            // Zeile aufgrund von ',' trennen
            sTmpArr = sTmp.Split(',');
            if (iZeile == 0)
            {
               // erste Zeile --> Spaltenzahl festlegen
               iSpalten = sTmpArr.GetLength(0);
               if (iSpalten > Konstante.MAX_SPALTE)
               {
                  sTmpAusn = String.Format("Es darf nicht mehr als {0} Spalten geben.", 
                     Konstante.MAX_SPALTE);
                  throw new FileLoadException(sTmpAusn);
               }
            }
            else
            {
               // prüfen ob einheitliche Spaltenlänge
               if (sTmpArr.GetLength(0) != iSpalten)
               {
                  throw new FileLoadException("Unterschiedliche Spaltenanzahl in Datei gefunden.");
               }
            }

            // über alle Einträge im getrennten Zeilenstring
            for (int k=0;k<sTmpArr.GetLength(0);k++)
            {
               // Leerzeichen vorne und hinten abschneiden / Groß- / Kleinschreibung nicht beachten
               sZelle = sTmpArr[k].Trim().ToUpper();

               if (sZelle.Equals("S"))
               {
                  // Startzelle gefunden
                  if (bStartgefunden)
                  {
                     // Startzelle wurde bereits gefunden
                     throw new FileLoadException("Es darf nur eine Startzelle definiert sein.");
                  }

                  // Startzelle wird in Feld als 0 gespeichert
                  iTmp = 0;
                  bStartgefunden = true;
                  
                  //Startknoten festlegen
                  e.si = iZeile;
                  e.sk = k;
                  e.iStartknoten = iZeile * iSpalten + k;
               }
               else if (sZelle.Equals("Z"))
               {
                  // Zielzelle gefunden
                  if (bZielgefunden)
                  {
                     // Ziel bereits gefunden
                     throw new FileLoadException("Es darf nur eine Zielzelle definiert sein.");
                  }

                  // Zielzelle wird in Feld als 0 gespeichert
                  iTmp = 0;
                  bZielgefunden = true;

                  //Zielknoten festlegen
                  e.zi = iZeile;
                  e.zk = k;
                  e.iZielknoten = iZeile * iSpalten + k;
               }
               else
               {
                  // wenn nicht S oder Z muss ein Zahlenwert vorliegen:
                  try
                  {
                     iTmp = Int32.Parse(sZelle);
                  }
                  catch
                  {
                     sTmpAusn = "Es sind nur ganzzahlige Zahlenwerte " 
                        + "zwiwschen 1 und 9 und S,Z in den Zellen erlaubt.";
                     throw new ArithmeticException(sTmp); 
                  }

                  // liegt Zahlenwert im definierten Bereich?
                  if (iTmp < 1 || iTmp > 9)
                  {
                     sTmpAusn = "Es sind nur Zahlenwerte"
                        + "zwischen 1 und 9 erlaubt.";
                     throw new FileLoadException(sTmpAusn); 
                  }
               }

               // gelesenen Wert in temporären Feld speichern
               tmpFeld[iZeile, k] = iTmp;
            }

            iZeile++;   // nächste Zeile
         }
         sr.Close();

         // sind Start / Ziel gefunden?
         if (!bStartgefunden)
         {
            throw new FileLoadException("In der Datei muss eine Startzelle angegeben sein.");
         }
         
         if (!bZielgefunden)
         {
            throw new FileLoadException("In der Datei muss eine Zielzelle angegeben sein.");
         }

         //tmpFeld in Ergebnis - Feld übertragen
         e.Feld = new int[iZeile, iSpalten];
         for (int i = 0; i < iZeile; i++)
         {
            for (int k = 0; k < iSpalten; k++)
            {
               e.Feld[i, k] = tmpFeld[i, k];
            }
         }
      }

      /// <summary>
      /// wandelt ein Feld in eine Adjazenzmatrix um
      /// </summary>
      /// <remarks>
      /// Hinweise:  Die Knoten werden Zeilenweise eingelesen, nummeriert. Also erst Zeile 0, 
      ///            dann Zeile 1, usw...
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="e">Ein Ergebnis Objekt</param>
      public void FeldUmwandeln(Ergebnis e)
      {
         //AdjazenzMatrix erzeugen, ein Feld der Größe m*n x m*n
         e.AdjazenzMatrix = new int[e.Feld.GetLength(0) * e.Feld.GetLength(1), 
                                    e.Feld.GetLength(0) * e.Feld.GetLength(1)];

         int iZeile, iSpalte, kZeile, kSpalte;
         int iBreiteFeld = e.Feld.GetLength(1);

         // über alle Zeilen der Adjazenmatrix
         for (int i = 0; i < e.AdjazenzMatrix.GetLength(1); i++)
         {
            // Spalte und Zeile im Feld des Zeilen - Knotens berechnen
            iZeile = (int)(i / iBreiteFeld);
            iSpalte = i % iBreiteFeld;
            
            // über alle Spalten der Adjazenzmatrix
            for (int k = 0; k < e.AdjazenzMatrix.GetLength(0); k++)
            {
               // Spalte und Zeile im Feld des Spalten - Knotens berechnen
               kZeile = (int)(k / iBreiteFeld);
               kSpalte = k % iBreiteFeld;
               
               if (i == k)
               {
                  // Weg auf sich selbst ist 0
                  e.AdjazenzMatrix[i, k] = 0;
               }
               else
               {
                  // alle anderen Verbindungen, prüfen ob Knoten benachbart
                  if ((iZeile == kZeile && Math.Abs(iSpalte - kSpalte) == 1) ||
                      (iSpalte == kSpalte && Math.Abs(iZeile - kZeile) == 1))
                  {
                     e.AdjazenzMatrix[i, k] = e.Feld[kZeile, kSpalte];
                  }
                  else
                  {
                     // nicht benachbart, unendlich zuweisen
                     e.AdjazenzMatrix[i, k] = Konstante.UNENDLICH;
                  }
               }
            }
         }
      }
   }
}
