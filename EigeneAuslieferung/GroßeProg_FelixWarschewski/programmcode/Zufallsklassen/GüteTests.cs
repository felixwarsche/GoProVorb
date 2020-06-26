using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    /// <summary>
    /// Beschreibt eine Schnittstelle für verschiedene Güte-Testverfahren
    /// </summary>
    public interface GüteTests
    {
        /// <summary>
        /// Berechnet einen Richtwert, um eine Aussage über die Güte eines Verfahrens machen zu können
        /// </summary>
        /// <param name="k">0 oder beliebig</param>
        /// <param name="anz">Sequenzlänge</param>
        /// <returns></returns>
        double Berechne(int k, int anz);
    }

    /// <summary>
    /// Beschreibt das Güte-Testverfahren
    /// </summary>
    public enum GüteTestverfahren
    {
        SerielleAutokorrelation,
        SequenzUpDown,
        Eigen
    }
}
