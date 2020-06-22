using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb
{
    class SerielleAutokorrelation : GüteTests
    {
        long[] zahlen;
        long mittelwert;

        public SerielleAutokorrelation(long[] zahlen, long mittelwert)
        {
            this.zahlen = zahlen;
            this.mittelwert = mittelwert;
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
