using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public interface Zufallsbibliothek
    {
        double GeneriereZufallszahl();
        double GeneriereGleichverteilteZufallszahl01();
        Generatorenklasse getArt();
        double getM();
        Verteilung Verteilung {get;set; }
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
