using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb
{
    class SerielleAutokorrelation : GüteTests
    {
        long[] zahlen;
        long mittelwert;

        public SerielleAutokorrelation(long[] zahlen)
        {
            this.zahlen = zahlen;
            this.mittelwert = berechneMittelwert();
        }

        /// <summary>
        /// Berechnet den Mittelwert für das gegebene Zahlen-Array
        /// </summary>
        /// <returns></returns>
        private long berechneMittelwert()
        {
            long summe = 0;
            foreach(var zahl in zahlen)
            {
                summe += zahl;
            }
            return summe / zahlen.Length;
        }

        /// <summary>
        /// Errechnet den Korrelationswert für das Array von zahlen
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public double berechne(int k)
        {
            long zaehler = 0;
            long nenner = 0;
            int i = 0;
            while(i < zahlen.Length - k)
            {
                zaehler += (this.zahlen[i] - mittelwert) * (this.zahlen[i + k] - mittelwert);
                nenner += (long)Math.Pow(this.zahlen[i] - mittelwert, 2);
            }
            return zaehler / nenner;
        }
    }
}
