using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public interface Zufallsbibliothek
    {
        double GeneriereZufallszahl();
        double GeneriereGleichverteilteZufallszahl01();
        Generatorenklasse GetArt();
        double GetM();
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
