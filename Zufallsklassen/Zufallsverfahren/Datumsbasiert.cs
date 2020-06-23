using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class Datumsbasiert : Zufallsbibliothek
    {
        Generatorenklasse art;
        int i;
        double m;

        public Datumsbasiert()
        {
            i = 1;
            m = 1000;
            art = Generatorenklasse.Datumsbasiert;
        }

        /// <summary>
        /// Generiert auf Basis der jetzigen Zeit und der Systemzeit eine Zufallszahl in Abhängigkeit der i-ten Zufallszahl, welche generiert wird.
        /// </summary>
        /// <returns></returns>
        public double GeneriereZufallszahl()
        {
            DateTime now = DateTime.Now;
            double additionen = now.Second + now.Minute + now.Hour + now.Day + now.Month + System.Environment.TickCount;

            return (double)((additionen * i) % m) / m; 
        }

        public Generatorenklasse getArt()
        {
            return this.art;
        }

        public double getM()
        {
            return this.m;
        }
    }
}
