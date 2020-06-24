using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class SequenzUpDownTest : GüteTests
    {
        Zufallsbibliothek bibliothek;
        string name;
        
        public SequenzUpDownTest(Zufallsbibliothek bib)
        {
            this.name = "SequenzUpDownTest";
            this.bibliothek = bib;
        }

        /// <summary>
        /// Berechnet die Anzahl der Bitfolgen mit der Länge K.
        /// </summary>
        /// <param name="k">Hat in diesem kontext erstmal keine Bedeutung, da über alle k gegangen wird.</param>
        /// <param name="anz">Bestimmt die Sequenzlänge des Tests. Also die Anzahl der untersuchten Zufallszahlen</param>
        /// <returns></returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz];
            for(int i = 0; i < anz; i++) //Erstelle Zahlenarray
            {
                zahlen[i] = bibliothek.GeneriereZufallszahl();
            }
            int[] bitmaske = new int[zahlen.Length];
            for(int i=0; i<zahlen.Length-1; i++) //Erstelle Bitmaske
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

            k = 1;
            int summe = 0; //Die Summer aller gefundenen Zahlen
            double differenz = 0; //Die Differenz für jedes k mit Nopt
            while(summe < anz - 2 || k > anz/2) //Abbruchbedingungen um Endlosschleifen zu verhindern
            {
                int counter = 0; //Suche Bitfolgen
                int kBitFolgen = 0;
                for (int i = 0; i < bitmaske.Length - 1; i++)
                {
                    counter++;
                    if (bitmaske[i] != bitmaske[i + 1])
                    {
                        if (counter == k)
                        {
                            kBitFolgen++;
                        }
                        counter = 0;
                    }
                }
                differenz += BerechneNopt(k, anz) - kBitFolgen;
                summe += k * kBitFolgen;
                k++;
            }

            return differenz;
        }

        private double BerechneNopt(int k, int anz)
        {
            double zaehler = (Math.Pow(k, 2) + 3 * k + 1) * anz - (Math.Pow(k, 3) + 3 * Math.Pow(k, 2) - k - 4);
            double nenner = Fakultaet(k + 3) / 2;
            return zaehler / nenner;
        }

        private double Fakultaet(double zahl)
        {
            if (zahl < 2)
                return zahl;
            return zahl * Fakultaet(zahl - 1);
        }
    }
}
