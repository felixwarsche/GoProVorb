using System;
using System.IO;
using Zufallsklassen;

namespace GroProVorb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Einlesen der Daten
            Benutzereingaben input = new Benutzereingaben();

            Console.WriteLine("LCG Verfahren in der Reihenfolge der Tabelle2 auf Seite 3");
            Zufallsbibliothek b = new LCG(LCGVerfahrenParameter.AnsiC);
            Console.WriteLine(b.GeneriereZufallszahl());
            b = new LCG(LCGVerfahrenParameter.MinimalStandard, new Standardnormalverteilung());
            Console.WriteLine(b.GeneriereZufallszahl());
            b = new LCG(LCGVerfahrenParameter.RANDU);
            Console.WriteLine(b.GeneriereZufallszahl());
            b = new LCG(LCGVerfahrenParameter.SIMSCRIPT);
            Console.WriteLine(b.GeneriereZufallszahl());
            b = new LCG(LCGVerfahrenParameter.NAGsLCG);
            Console.WriteLine(b.GeneriereZufallszahl());
            b = new LCG(LCGVerfahrenParameter.MaplesLCG);
            Console.WriteLine(b.GeneriereZufallszahl());

            Console.WriteLine("Verwendung des eigenen Zufallsgenerator");
            b = new Datumsbasiert();
            Console.WriteLine(b.GeneriereZufallszahl());

            Console.WriteLine("GüteTests");
            GüteTests t = new SerielleAutokorrelation(b);
            var test = t.Berechne(5,20);
            Console.WriteLine(t.Berechne(5,20));
            t = new SequenzUpDownTest(b);
            Console.WriteLine(t.Berechne(5,20));
            Console.ReadKey();
        }
    }
}
