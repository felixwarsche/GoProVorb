using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class Gleichverteilung : Verteilung
    {
        public V Art { get; private set; }

        public double Mittelwert { get { return 0.5; } }
        public Gleichverteilung()
        {
            Art = V.Gleichverteilung;
        }

        public double Transformiere(double x)
        {
            return x;
        }
    }
}
