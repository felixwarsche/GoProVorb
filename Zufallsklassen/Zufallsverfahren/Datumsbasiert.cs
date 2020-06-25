using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    /// <summary>
    /// Erzeugt einen eigenen datumsbasierten Zufallszahlengenerator
    /// </summary>
    public class Datumsbasiert : Zufallsbibliothek
    {
        Generatorenklasse Art;
        int i;
        double m;

        /// <summary>
        /// Erstellt eine Instanz des datumsbasierten Zufallszahlengenerator
        /// </summary>
        public Datumsbasiert()
        {
            i = 0;
            m = 1000;
            Art = Generatorenklasse.Datumsbasiert;
        }

        public Verteilung Verteilung { get; set; }

        /// <summary>
        /// Generiert eine Gleichverteilte Zufallszahl im Bereich 0-1
        /// </summary>
        /// <returns>Zufallszahl im Intervall 0-1</returns>
        public double GeneriereGleichverteilteZufallszahl01()
        {
            DateTime now = DateTime.Now;
            double additionen = now.Second + now.Minute + now.Hour + now.Day + now.Month + System.Environment.TickCount;
            this.i++;

            return (double)((additionen * i) % m) / m;  //Berechnet eine Zufallszahl, indem die aufsummierten Zeitangaben mit dem Wert für die Anzahl der bisher generierten Zufallzahlen multipliziert 
                                                        //werden und dann durch das Modul auf ein Intervall zwischen 0 und 1 geregelt werden
        }

        /// <summary>
        /// Generiert auf Basis der jetzigen Zeit und der Systemzeit eine Zufallszahl[0,1] in Abhängigkeit der i-ten Zufallszahl, welche generiert wird.
        /// </summary>
        /// <returns>Zufallszahl im Intervall 0-1</returns>
        public double GeneriereZufallszahl()
        {
            double x = GeneriereGleichverteilteZufallszahl01();

            return Verteilung.Transformiere(x);
        }

        public Generatorenklasse GetArt()
        {
            return this.Art;
        }

        public double GetM()
        {
            return this.m;
        }
    }
}
