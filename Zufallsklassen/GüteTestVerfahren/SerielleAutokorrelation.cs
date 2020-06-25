using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    /// <summary>
    /// Erzeugt eine Serielle Autokorrelation
    /// </summary>
    public class SerielleAutokorrelation : GüteTests
    {
        Zufallsbibliothek Bibliothek;
        string Name;
        
        /// <summary>
        /// Erstellt die Instanz einer seriellen Autokorrelation
        /// </summary>
        /// <param name="b"></param>
        public SerielleAutokorrelation(Zufallsbibliothek b)
        {
            this.Name = "SerielleAutokorrelation";
            this.Bibliothek = b;
        }

        /// <summary>
        /// Errechnet den Korrelationswert für das Zufallsgenerierte Array
        /// </summary>
        /// <param name="k">Abstand der verglichenen Zufallszahlen</param>
        /// <returns>Korrelationswert des Zufallszahlengenerators</returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz];
            for(int i=0; i<anz; i++)
            {
                zahlen[i] = Bibliothek.GeneriereZufallszahl();
            }

            if(k == 0)//wenn kein spezifisches k gewählt wurde, wird ein zufälliger Abstand zweier Zahlen ausgerechnet
            {
                int punkt1 = (int)(Bibliothek.GeneriereGleichverteilteZufallszahl01() * anz);
                int punkt2 = (int)(Bibliothek.GeneriereGleichverteilteZufallszahl01() * anz);
                k = Math.Abs(punkt1 - punkt2);
            }
            double mittelwert = Bibliothek.Verteilung.Mittelwert; // Berechnet Mittelwert aus allen Zufallszahlen
            double zaehler = 0;
            double nenner = 0;
            int j = 0;
            while(j < zahlen.Length - k) // Führt die Serielle Autokorrelation durch
            {
                zaehler += (zahlen[j] - mittelwert) * (zahlen[j + k] - mittelwert);
                nenner += Math.Pow(zahlen[j] - mittelwert, 2);
                j++;
            }
            return (double) zaehler / nenner;
        }
    }
}
