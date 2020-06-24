using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public interface Verteilung
    {
        double Transformiere(double x);
        V Art { get; }
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
