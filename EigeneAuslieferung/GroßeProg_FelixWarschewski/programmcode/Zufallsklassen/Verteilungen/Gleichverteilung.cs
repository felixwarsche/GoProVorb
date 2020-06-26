using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    /// <summary>
    /// Erzeugt eine Gleichverteilung
    /// </summary>
    public class Gleichverteilung : Verteilung
    {
        public V Art { get; private set; }

        public double Mittelwert { get { return 0.5; } }

        /// <summary>
        /// Erstellt eine Instanz der Gleichverteilung
        /// </summary>
        public Gleichverteilung()
        {
            Art = V.Gleichverteilung;
        }

        /// <summary>
        /// Transformiert in die Gleichverteilung
        /// Da aber nur gleichverteilte Zahlen bisher übergeben werden, muss nichts transformiert werden.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Transformiere(double x)
        {
            return x;
        }
    }
}
