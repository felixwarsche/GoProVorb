using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class Datumsbasiert : Zufallsbibliothek
    {
        int i;

        public Datumsbasiert()
        {
            i = 1;
        }

        /// <summary>
        /// Generiert auf Basis der jetzigen Zeit und der Systemzeit eine Zufallszahl in Abhängigkeit der i-ten Zufallszahl, welche generiert wird.
        /// </summary>
        /// <returns></returns>
        public double GeneriereZufallszahl()
        {
            DateTime now = DateTime.Now;
            double additionen = now.Second + now.Minute + now.Hour + now.Day + now.Month + System.Environment.TickCount;
            double m = 1000;

            return (double)((additionen * i) % m) / m; 
        }
    }
}
