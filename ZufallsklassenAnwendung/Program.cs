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
            dateipfad = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "\\Ergebnisse"
               + @"\" + Path.GetFileNameWithoutExtension(dateipfad) + "_Ergebnisse.txt";


            string[] save = new string[999];
            save[0] = "Die Sequenzlänge der folgenden Ergebnisse beträgt: " + eingabe.Sequenzlänge + " und die Ordnung(falls ungleich 0) beträgt: " + eingabe.K;
            save[1] = "Die angewandte Verteilung der Zufallszahlen ist die " + eingabe.Verteilung.ToString();
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
        }
    }
}
