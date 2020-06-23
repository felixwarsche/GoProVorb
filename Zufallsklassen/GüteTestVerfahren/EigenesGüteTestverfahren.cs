using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class EigenesGüteTestverfahren : GüteTests
    {
        Zufallsbibliothek bibliothek;
       
        public EigenesGüteTestverfahren(Zufallsbibliothek bib)
        {
            this.bibliothek = bib;
        }

        /// <summary>
        /// Angelehnt an die SequenzUpDown werden hier die Wechsel der größen geprüft und dann im Anschluss durch die Anzahl der möglichen Wechsel dividiert.
        /// Soll Aufschluss über die mittleren Wechsel geben. Je höher desto besser ! 
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz];
            for (int i = 0; i < anz; i++)
            {
                zahlen[i] = bibliothek.GeneriereZufallszahl();
            }
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
