using System;
using System.IO;
using System.Reflection;
using Zufallsklassen;

namespace GroProVorb
{
    class Program
    {
        static void Main(string[] args)
        {
            string dateipfad;
            if (args.Length > 0)
            {
                // Parameter zum einlesen der Datei
                dateipfad = args[0];
            }
            else
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                string path = System.IO.Path.GetDirectoryName(asm.Location);
                // Standard Input verwenden
                dateipfad = path + "\\Tests\\AlleTestsMitSequenzlänge100000.txt";
            }
            //Einlesen der Daten
            Eingabe eingabe = new Eingabe();
            eingabe.Einlesen(dateipfad);

            // Ausgabe-Dateinamen festlegen (NameEingabedatei_Ergebnisse.txt)
            dateipfad = Path.GetDirectoryName(dateipfad)
               + @"\" + Path.GetFileNameWithoutExtension(dateipfad) + "_Ergebnisse.txt";


            string[] save = new string[999];
            save[0] = "Die Sequenzlänge der folgenden Ergebnisse beträgt: " + eingabe.Sequenzlänge + " und die Ordnung(falls ungleich 0) beträgt: " + eingabe.K;
            save[0] = "Die angewandte Verteilung der Zufallszahlen ist die " + eingabe.Verteilung.ToString();
            int i = 2;
            foreach (var generator in eingabe.Zufallsgeneratoren)
            {
                foreach (var verfahren in eingabe.GüteTestVerfahren)
                {
                    double ausgabe = double.MaxValue;
                    if (verfahren == GüteTestverfahren.SerielleAutokorrelation)
                    {
                        var v = new SerielleAutokorrelation(generator);
                        ausgabe = v.Berechne(eingabe.K, eingabe.Sequenzlänge);
                    }
                    else if (verfahren == GüteTestverfahren.SequenzUpDown)
                    {
                        var v = new SequenzUpDownTest(generator);
                        ausgabe = v.Berechne(eingabe.K, eingabe.Sequenzlänge);
                    }
                    else if (verfahren == GüteTestverfahren.Eigen)
                    {
                        var v = new EigenesGüteTestverfahren(generator);
                        ausgabe = v.Berechne(eingabe.K, eingabe.Sequenzlänge);
                    }
                    string a = "Generator: " + generator.GetArt().ToString() + ", Verfahren: " + verfahren.ToString() + ", Wert: " + ausgabe;
                    Console.WriteLine(a);
                    save[i] = a;
                    i++;
                }
            }
            Ausgabe output = new Ausgabe(dateipfad);
            output.Schreiben(save);
            //Console.WriteLine("LCG Verfahren in der Reihenfolge der Tabelle2 auf Seite 3");
            //var a = new LCG(LCGVerfahrenParameter.AnsiC);
            //Console.WriteLine(a.GeneriereZufallszahl());
            //var b = new LCG(LCGVerfahrenParameter.MinimalStandard, new Standardnormalverteilung());
            //Console.WriteLine(b.GeneriereZufallszahl());
            //var c = new LCG(LCGVerfahrenParameter.RANDU);
            //Console.WriteLine(c.GeneriereZufallszahl());
            //var d = new LCG(LCGVerfahrenParameter.SIMSCRIPT);
            //Console.WriteLine(d.GeneriereZufallszahl());
            // var e = new LCG(LCGVerfahrenParameter.NAGsLCG);
            //Console.WriteLine(e.GeneriereZufallszahl());
            //var f = new LCG(LCGVerfahrenParameter.MaplesLCG);
            //Console.WriteLine(f.GeneriereZufallszahl());

            //Console.WriteLine("Verwendung des eigenen Zufallsgenerator");
            //var date = new Datumsbasiert();
            //Console.WriteLine(date.GeneriereZufallszahl());

            //Console.WriteLine("GüteTests");
            //var t = new SerielleAutokorrelation(a);
            //Console.WriteLine(t.Berechne(5,20));
            //var u = new SequenzUpDownTest(a);
            //Console.WriteLine(t.Berechne(5,20));
            Console.ReadKey();
        }
    }
}
