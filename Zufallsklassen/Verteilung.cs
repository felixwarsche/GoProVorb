using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public interface Verteilung
    {
        double Transformiere(double x, double y);
        V getArt();
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
