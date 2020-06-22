﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb.GüteTestVerfahren
{
    class SequenzUpDownTest : GüteTests
    {
        long[] zahlen;

        /// <summary>
        /// Berechnet die Anzahl der Bitfolgen mit der Länge K.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public double berechne(int k)
        {
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
