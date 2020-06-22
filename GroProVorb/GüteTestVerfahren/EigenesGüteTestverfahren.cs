using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb.GüteTestVerfahren
{
    class EigenesGüteTestverfahren : GüteTests
    {
        long[] zahlen;

        /// <summary>
        /// Angelehnt an die SequenzUpDown werden hier die Wechsel der größen geprüft und dann im Anschluss durch die Anzahl der möglichen Wechsel dividiert.
        /// Soll Aufschluss über die mittleren Wechsel geben. Je höher desto besser ! 
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public double berechne(int k)
        {
            int[] bitmaske = new int[zahlen.Length];
            for (int i = 0; i < zahlen.Length - 1; i++)
            {
                if (zahlen[i] < zahlen[i + 1])
                {
                    bitmaske[i] = 1;
                }
                else
                {
                    bitmaske[i] = 0;
                }
            }
            long summe = 0;
            for(int i=0; i<bitmaske.Length-1; i++)
            {
                if (bitmaske[i] != bitmaske[i + 1])
                {
                    summe++;
                }
            }
            return summe / zahlen.Length;
        }
    }
}
