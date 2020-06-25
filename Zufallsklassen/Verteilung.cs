using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    /// <summary>
    /// Eine Schnittstelle für die angewendeten Verteilungen der Zufallszahlengeneratoren
    /// </summary>
    public interface Verteilung
    {
        /// <summary>
        /// Transformiert die x Variable in die entsprechende Verteilung
        /// </summary>
        /// <param name="x">Wert</param>
        /// <returns>Transformiertes x</returns>
        double Transformiere(double x);

        /// <summary>
        /// Beschreibt die Art der Verteilung
        /// </summary>
        V Art { get; }

        /// <summary>
        /// Beschreibt den Mittelwert, den die jeweilige Verteilung aufweist
        /// </summary>
        double Mittelwert { get; }
    }

    /// <summary>
    /// Beschreibt die Art der Verteilung
    /// </summary>
    public enum V
    {
        Gleichverteilung,
        Standardnormalverteilung
    }
}
