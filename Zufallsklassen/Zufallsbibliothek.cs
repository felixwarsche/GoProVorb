using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public interface Zufallsbibliothek
    {
        double GeneriereZufallszahl();
        Generatorenklasse getArt();
        double getM();
    }

    public enum Generatorenklasse {
        LCG,
        AnsiC,
        MinimalStandard,
        RANDU,
        SIMSCRIPT,
        NAGsLCG,
        MaplesLCG,
        Datumsbasiert
    }

}
