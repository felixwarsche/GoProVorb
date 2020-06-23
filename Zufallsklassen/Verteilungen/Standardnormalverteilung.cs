using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class Standardnormalverteilung : Verteilung
    {
        private int[] verteilungsparameter;
        private V art;

        public Standardnormalverteilung()
        {
            verteilungsparameter = new int[] { 0, 1 };
            art = V.Standardnormalverteilung;
        }

        public V getArt()
        {
            return art;
        }

        /// <summary>
        /// Polarmethode wie im Arbeitsbogen beschrieben
        /// </summary>
        /// <param name="x">Zufallszahl</param>
        /// <param name="b">Bibliothek um Zufallszahlen zu generieren</param>
        /// <returns>Returnt eine von den beiden generierten unabhängigen Standardnormalverteilten Zufallszahlen (x*p; y*p) Skaliert mit p</returns>
        public double Transformiere(double x, double y)
        {
            var q = Math.Pow(x, 2) + Math.Pow(y, 2);
            var p = Math.Sqrt((-2 * Math.Log(q)) / q);
            return x * p;
        }
    }
}
