using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class SequenzUpDownTest : GüteTests
    {
        Zufallsbibliothek Bibliothek;
        string Name;

        public SequenzUpDownTest(Zufallsbibliothek bib)
        {
            this.Name = "SequenzUpDownTest";
            this.Bibliothek = bib;
        }

        /// <summary>
        /// Berechnet die Anzahl der Bitfolgen mit der Länge K.
        /// </summary>
        /// <param name="k">Wenn 0 dann werden alle möglichen k überprüft, ansonsten nur das angegebene k.</param>
        /// <param name="anz">Bestimmt die Sequenzlänge des Tests. Also die Anzahl der untersuchten Zufallszahlen</param>
        /// <returns></returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz];
            for (int i = 0; i < anz; i++) //Erstelle Zahlenarray
            {
                zahlen[i] = Bibliothek.GeneriereZufallszahl();
            }
            int[] bitmaske = new int[zahlen.Length-1];
            for (int i = 0; i < zahlen.Length - 1; i++) //Erstelle Bitmaske
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

            Dictionary<int, int> kettenlaengen = new Dictionary<int, int>();
            int kettenlaenge = 1; //Speichert die Anzahl der jetzigen Folge
            int vorherigesBit = bitmaske[0];
            for (int i = 1; i < bitmaske.Length; i++)
            {
                if (vorherigesBit == bitmaske[i]) 
                {
                    kettenlaenge++;
                }
                else //Prüft, ob ein Wechsel stattfindet um den Counter zurückzusetzen und ggfls. eine gefundene Bitfolge mit k Elementen zu speichern
                {
                    if (kettenlaengen.ContainsKey(kettenlaenge))
                    {
                        kettenlaengen[kettenlaenge]++;
                    }
                    else
                    {
                        kettenlaengen[kettenlaenge] = 1;
                    }
                    kettenlaenge = 1;
                    vorherigesBit = bitmaske[i];
                }
            }
            if (k == 0)
            {
                double differenz = 0;
                foreach (var eintrag in kettenlaengen)
                {
                    differenz += Math.Abs(BerechneNopt(eintrag.Key, anz) - eintrag.Value); //Errechnet die Differenz der gefundenen Zahlenketten mit dem des optimalen Wertes
                }
                return differenz;
            }
            else
            {
                return Math.Abs(BerechneNopt(k, anz) - kettenlaengen[k]);
            }
        }

        /// <summary>
        /// Berechnet das optimale Ergebnis für bestimmte Kettenlängen
        /// </summary>
        /// <param name="k">Länge der Kette</param>
        /// <param name="anz">Sequenzlänge</param>
        /// <returns>Richtwert</returns>
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
