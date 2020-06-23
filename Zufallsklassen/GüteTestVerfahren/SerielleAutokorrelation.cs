using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class SerielleAutokorrelation : GüteTests
    {
        Zufallsbibliothek bibliothek;
        
        public SerielleAutokorrelation(Zufallsbibliothek b)
        {
            this.bibliothek = b;
        }

        /// <summary>
        /// Berechnet den Mittelwert für das gegebene Zahlen-Array
        /// </summary>
        /// <returns></returns>
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
        /// <param name="k"></param>
        /// <returns></returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz];
            for(int i=0; i<anz; i++)
            {
                zahlen[i] = bibliothek.GeneriereZufallszahl();
            }
            double mittelwert = BerechneMittelwert(zahlen);
            double zaehler = 0;
            double nenner = 0;
            int j = 0;
            while(j < zahlen.Length - k)
            {
                zaehler += (zahlen[j] - mittelwert) * (zahlen[j + k] - mittelwert);
                nenner += Math.Pow(zahlen[j] - mittelwert, 2);
                j++;
            }
            return (double) zaehler / nenner;
        }
    }
}
