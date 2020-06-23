using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class Gleichverteilung : Verteilung
    {
        private V art;

        public Gleichverteilung()
        {
            art = V.Gleichverteilung;
        }

        public V getArt()
        {
            return art;
        }

        public double Transformiere(double x, double y)
        {
            throw new NotImplementedException();
        }
    }
}
