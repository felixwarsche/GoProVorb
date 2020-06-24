using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class EigenesGüteTestverfahren : GüteTests
    {
        Zufallsbibliothek Bibliothek;
        string Name;

        public EigenesGüteTestverfahren(Zufallsbibliothek bib)
        {
            this.Name = "Eigen";
            this.Bibliothek = bib;
        }

        /// <summary>
        /// Angelehnt an die SequenzUpDown werden hier die Wechsel der Größen geprüft und dann im Anschluss durch die Anzahl der möglichen Wechsel dividiert.
        /// Soll Aufschluss über die mittleren Wechsel geben. Wenn die Werte um die 0,5 oder höher liegen, liegt eine gute Güte vor! Ansonsten eine schlechte, da die Folgen sehr lang sind.
        /// </summary>
        /// <param name="k">Sequenzlänge</param>
        /// <returns>Den Mittelwert aller Kettenwechsel.</returns>
        public double Berechne(int k, int anz)
        {
            double[] zahlen = new double[anz]; //Zufallszahlen erstellen
            for (int i = 0; i < anz; i++)
            {
                zahlen[i] = Bibliothek.GeneriereZufallszahl();
            }
            int[] bitmaske = new int[zahlen.Length]; //Bitmaske erstellen
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
            for(int i=0; i<bitmaske.Length-1; i++) //Bildet Summe der Wechsel
            {
                if (bitmaske[i] != bitmaske[i + 1])
                {
                    summe++;
                }
            }
            return (double)summe / bitmaske.Length; //Mittelwert der Wechsel
        }
    }
}
