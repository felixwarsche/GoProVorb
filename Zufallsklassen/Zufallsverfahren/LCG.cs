using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class LCG : Zufallsbibliothek
    {
        private string name;
        private double[] parameter;
        private Verteilung verteilung;

        public LCG(double[] p)
        {
            verteilung = new Gleichverteilung();
            parameter = p;
        }

        public LCG(double[] p, Verteilung v)
        {
            parameter = p;
            verteilung = v;
        }

        /// <summary>
        /// Generiert eine Zufallszahl in Abhängikeit der gegebenen Parameter und speichert den xi-Parameter nach Schleifendurchlauf.
        /// 
        /// Es wird auch abgefragt, welche Verteilung hier angewendet werden soll und dann ggflls. transformiert.
        /// </summary>
        /// <returns>Zufallszahl</returns>
        public double GeneriereZufallszahl()
        {
            double x = errechne();

            if (verteilung.getArt() == V.Standardnormalverteilung)
            {
                double q = 0;
                double y = 0;
                do
                {
                    y = errechne() * Math.Sqrt(1 - Math.Pow(x, 2));
                    q = Math.Pow(x, 2) + Math.Pow(y, 2);
                } while (q == 0 || q >= 1);
                x = verteilung.Transformiere(x, y);
            }
            return x;
        }

        /// <summary>
        /// Berechnet eine Gleichverteilte Zufallszahl nach dem Prinzip der erhaltenen Parameter
        /// </summary>
        /// <returns></returns>
        private double errechne()
        {
            double m = parameter[0]; double a = parameter[1]; double c = parameter[2]; double x = parameter[3];
            double wertBisMudolo = ((a * x) + c) % m;
            double wert = (double)wertBisMudolo / m;
            parameter[3] = wertBisMudolo;
            return wert;
        }
    }


    public class LCGVerfahrenParameter
    {
        public static readonly double[] AnsiC = new double[] { Math.Pow(2, 31), 1103515245, 12345, 12345 };
        public static readonly double[] MinimalStandard = new double[] { Math.Pow(2, 31) - 1, 16807, 0, 1 };
        public static readonly double[] RANDU = new double[] { Math.Pow(2, 31), 65539, 0, 1 };
        public static readonly double[] SIMSCRIPT = new double[] { Math.Pow(2, 31) - 1, 630360016, 0, 1 };
        public static readonly double[] NAGsLCG = new double[] { Math.Pow(2, 59), Math.Pow(13, 13), 0, 123456789 };
        public static readonly double[] MaplesLCG = new double[] { Math.Pow(10, 12) - 11, 427419669081, 0, 1 };
    }
}
