using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class SerielleAutokorrelation : GüteTests
    {
        Zufallsbibliothek bibliothek;
        string name;
        
        public SerielleAutokorrelation(Zufallsbibliothek b)
        {
            this.name = "SerielleAutokorrelation";
            this.bibliothek = b;
        }

        /// <summary>
        /// Berechnet den Mittelwert für das gegebene Zahlen-Array
        /// </summary>
        /// <returns>Mittelwert</returns>
        private double BerechneMittelwert(double[] zahlen)
        {
            double summe = 0;
            foreach(var zahl in zahlen)
            {
                summe += zahl;
            }
            return summe / zahlen.Length;
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
                zahlen[i] = bibliothek.GeneriereZufallszahl();
            }

            if(k == 0)//wenn kein spezifisches k gewählt wurde, wird ein zufälliger Abstand zweier Zahlen ausgerechnet
            {
                int punkt1 = (int)(bibliothek.GeneriereZufallszahl() * anz);
                int punkt2 = (int)(bibliothek.GeneriereZufallszahl() * anz);
                k = punkt1 - punkt2;
            }
            double mittelwert = BerechneMittelwert(zahlen); // Berechnet Mittelwert aus allen Zufallszahlen
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
