using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wegplanung
{
   /// <summary>
   /// Klasse zur Speicherung der eingelesenen Daten und der Ergebnisse
   /// </summary>
   /// <remarks>
   /// Historie:  22.05.2012  (awi) Erstellt
   /// </remarks>
   class Ergebnis
   {
      /// <summary>
      /// Feld wie eingelesen, S,Z als 0
      /// </summary>
      public int[,] Feld;                       
      
      /// <summary>
      /// Adjazenzmatrix für Berechnung der minimalen Lösung
      /// </summary>
      public int[,] AdjazenzMatrix;
      
      /// <summary>
      /// Zeile Start
      /// </summary>
      public int si;

      /// <summary>
      /// Spalte Start
      /// </summary>
      public int sk;
      
      /// <summary>
      /// Zeile Ziel
      /// </summary>
      public int zi;

      /// <summary>
      /// Spalte Ziel
      /// </summary>
      public int zk;

      /// <summary>
      /// Start als Knoten in der Adjazenzmatrix
      /// </summary>
      public int iStartknoten;

      /// <summary>
      /// Ziel als Knoten in der Adjazenzmatrix
      /// </summary>
      public int iZielknoten;

      /// <summary>
      /// Errechnete Kostenabschätzung
      /// </summary>
      public int iKostenAbsch;

      /// <summary>
      /// errechnete Minimalkosten
      /// </summary>
      public int iKostenMinimal;

      /// <summary>
      /// errechneter kürzester Weg
      /// </summary>
      public int[] kuerzesterWeg;

      /// <summary>
      /// Kommentar aus der Datei eingelesen
      /// </summary>
      public String sKommentar;

      /// <summary>
      /// Gibt die Zahl des Knotens in der Adjazenzmatrix für eine Zeile und Spalte des Feldes zurück
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="iZeile">Zeile im Feld des Objekts</param>
      /// <param name="iSpalte">Spalte im Feld des Objekts</param>
      /// <returns>die Zahl des Knotens als Integer</returns>
      public int KnotenZuIndex(int iZeile, int iSpalte)
      {
         // ist Adjazenzmatrix bereits erstellt?
         if(AdjazenzMatrix != null)
         {
            return iZeile * Feld.GetLength(1) + iSpalte;
         }
         else
         {
            throw new NullReferenceException("Es wurde noch keine Adjazenzmatrix angelegt");
         }
      }

      /// <summary>
      /// Gibt die Spalte zu einer Knotenzahl der Adjazenzmatrix
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="iKnoten">Der Knoten in der Adjazenzmatrix</param>
      /// <returns>die Spalte des Knotens im Feld als Integer</returns>
      public int SpalteZuKnoten(int iKnoten)
      {
         if (AdjazenzMatrix != null)
         {
            return iKnoten % Feld.GetLength(1);
         }
         else
         {
            throw new NullReferenceException("Es wurde noch keine Adjazenzmatrix angelegt");
         }
      }

      /// <summary>
      /// Gibt die Zeile zu einer Knotenzahl der Adjazenzmatrix
      /// </summary>
      /// <remarks>
      /// Historie:  22.05.2012  (awi) Erstellt
      /// </remarks>
      /// <param name="iKnoten">Der Knoten in der Adjazenzmatrix</param>
      /// <returns>die Zeile des Knotens im Feld als Integer</returns>
      public int ZeileZuKnoten(int iKnoten)
      {
         if (AdjazenzMatrix != null)
         {
            return (int)(iKnoten / Feld.GetLength(1));
         }
         else
         {
            throw new NullReferenceException("Es wurde noch keine Adjazenzmatrix angelegt");
         }
      }
   }
}
