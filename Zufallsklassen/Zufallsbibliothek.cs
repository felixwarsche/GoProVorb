using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    /// <summary>
    /// Eine Schnittstelle für verschiedene Zufallszahlengeneratoren
    /// </summary>
    public interface Zufallsbibliothek
    {
        /// <summary>
        /// Generiert eine Zufallszahl in Abhängigkeit der Verteilung
        /// </summary>
        /// <returns>Zufallszahl zwischen 0-1</returns>
        double GeneriereZufallszahl();

        /// <summary>
        /// Generiert eine Gleichverteilte Zufallszahl
        /// </summary>
        /// <returns>Zufallszahl zwischen 0-1</returns>
        double GeneriereGleichverteilteZufallszahl01();

        /// <summary>
        /// Beschreibt die Klassenart des Generators
        /// </summary>
        /// <returns>Generatorenklasse</returns>
        Generatorenklasse GetArt();

        /// <summary>
        /// Beschreibt das Modul der angewendeten Generatorenklasse
        /// </summary>
        /// <returns>Modulo</returns>
        double GetM();

        /// <summary>
        /// Beschreibt die Verteilung der Generatorenklasse
        /// </summary>
        Verteilung Verteilung { get;set; }
    }

    /// <summary>
    /// Bestimmt die Klasse des Zufallsgenerators
    /// </summary>
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
