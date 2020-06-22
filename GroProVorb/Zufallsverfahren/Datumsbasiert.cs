using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb
{
    class Datumsbasiert : Zufallsbibliothek
    {
        int i;

        public Datumsbasiert()
        {
            i = 0;
        }

        /// <summary>
        /// Generiert auf Basis der jetzigen Zeit und der Systemzeit eine Zufallszahl in Abhängigkeit der i-ten Zufallszahl, welche generiert wird.
        /// </summary>
        /// <returns></returns>
        public double GeneriereZufallszahl()
        {
            DateTime now = DateTime.Now;
            long additionen = now.Second + now.Minute + now.Hour + now.Day + now.Month + System.Environment.TickCount;
            long m = 1000;

            return ((additionen * i) % 1000) / 1000; 
        }
    }
}
