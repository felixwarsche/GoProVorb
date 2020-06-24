using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class Standardnormalverteilung : Verteilung
    {
        private Zufallsbibliothek Generator;
        private int[] Verteilungsparameter;

        public V Art { get; private set; }
        public double Mittelwert { get { return 0; } }

        public Standardnormalverteilung(Zufallsbibliothek generator)
        {
            this.Generator = generator;
            Verteilungsparameter = new int[] { 0, 1 };
            Art = V.Standardnormalverteilung;
        }

        /// <summary>
        /// Polarmethode wie im Arbeitsbogen beschrieben
        /// </summary>
        /// <param name="x">Zufallszahl</param>
        /// <param name="b">Bibliothek um Zufallszahlen zu generieren</param>
        /// <returns>Returnt eine von den beiden generierten unabhängigen Standardnormalverteilten Zufallszahlen (x*p; y*p) Skaliert mit p</returns>
        private double Transformiere(double x, double y)
        {
            var q = Math.Pow(x, 2) + Math.Pow(y, 2);
            var p = Math.Sqrt((-2 * Math.Log(q)) / q);
            return x * p;
        }

        /// <summary>
        /// Polarmethode wie im Arbeitsbogen beschrieben
        /// </summary>
        /// <param name="x">Zufallszahl</param>
        /// <param name="b">Bibliothek um Zufallszahlen zu generieren</param>
        /// <returns>Returnt eine von den beiden generierten unabhängigen Standardnormalverteilten Zufallszahlen (x*p; y*p) Skaliert mit p</returns>
        public double Transformiere(double x)
        {
            x = x * 2 - 1; // In [-1,1] Verteilung ändern
            double rand = 0;
            double q = 0;
            do
            {
                rand = Generator.GeneriereGleichverteilteZufallszahl01();
                rand = rand * 2 - 1;
                q = Math.Pow(x, 2) + Math.Pow(rand, 2);
            } while ( q == 0 || q >= 1);
            return Transformiere(x, rand);
        }
    }
}
