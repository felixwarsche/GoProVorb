using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class SequenzUpDownTest : GüteTests
    {
        Zufallsbibliothek bibliothek;
        
        public SequenzUpDownTest(Zufallsbibliothek bib)
        {
            this.bibliothek = bib;
        }

        /// <summary>
        /// Berechnet die Anzahl der Bitfolgen mit der Länge K.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz];
            for(int i = 0; i < anz; i++)
            {
                zahlen[i] = bibliothek.GeneriereZufallszahl();
            }
            int[] bitmaske = new int[zahlen.Length];
            for(int i=0; i<zahlen.Length-1; i++)
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
            int counter = 0;
            int kBitFolgen = 0;
            for (int i = 0; i < bitmaske.Length - 1; i++)
            {
                counter++;
                if(zahlen[i] != zahlen[i + 1])
                {
                    if(counter == k)
                    {
                        kBitFolgen++;
                    }
                    counter = 0;
                }
            }
            return kBitFolgen;
        }
    }
}
