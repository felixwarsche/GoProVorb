using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zufallsklassen;

namespace GroProVorb
{
    public class Eingabe
    {
        public List<Zufallsbibliothek> Zufallsgeneratoren;
        public List<GüteTestverfahren> GüteTestVerfahren;
        public int Sequenzlänge;
        public int K;
        public V Verteilung;

        public void Einlesen(string pfad)
        {
            try
            {
                using (var reader = new StreamReader(pfad))
                {
                    string[] line1 = reader.ReadLine().Split(','); //Zufallsgeneratoren
                    string[] line2 = reader.ReadLine().Split(','); //Testverfahren
                    string line3 = reader.ReadLine(); //SampleSize-Groeße der generierten Arrays
                    string line4 = reader.ReadLine(); //Sequenzlänge der testverfahren
                    string line5 = reader.ReadLine(); //Angabe ob Gleichverteilt oder Standardnormalverteilt
                    Verteilung = line5 == "0" ? V.Gleichverteilung : V.Standardnormalverteilung;
                    GeneratorenKonvertieren(line1);
                    VerfahrenKonvertieren(line2);
                    Sequenzlänge = int.Parse(line3);
                    K = int.Parse(line4);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Die Datei konnte nicht eingelesen werden. E: " + e);
            }
        }

        private void GeneratorenKonvertieren(string[] a)
        {
            Zufallsgeneratoren = new List<Zufallsbibliothek>();
            foreach(var generator in a)
            {
                if(generator == ((int)Generatorenklasse.AnsiC).ToString())
                {
                    Zufallsgeneratoren.Add(new LCG(Generatorenklasse.AnsiC, LCGVerfahrenParameter.AnsiC));
                }
                if (generator == ((int)Generatorenklasse.MinimalStandard).ToString())
                {
                    Zufallsgeneratoren.Add(new LCG(Generatorenklasse.MinimalStandard, LCGVerfahrenParameter.MinimalStandard));
                }
                if (generator == ((int)Generatorenklasse.RANDU).ToString())
                {
                    Zufallsgeneratoren.Add(new LCG(Generatorenklasse.RANDU, LCGVerfahrenParameter.RANDU));
                }
                if (generator == ((int)Generatorenklasse.SIMSCRIPT).ToString())
                {
                    Zufallsgeneratoren.Add(new LCG(Generatorenklasse.SIMSCRIPT, LCGVerfahrenParameter.SIMSCRIPT));
                }
                if (generator == ((int)Generatorenklasse.NAGsLCG).ToString())
                {
                    Zufallsgeneratoren.Add(new LCG(Generatorenklasse.NAGsLCG, LCGVerfahrenParameter.NAGsLCG));
                }
                if (generator == ((int)Generatorenklasse.MaplesLCG).ToString())
                {
                    Zufallsgeneratoren.Add(new LCG(Generatorenklasse.MaplesLCG, LCGVerfahrenParameter.MaplesLCG));
                }
                if (generator == ((int)Generatorenklasse.Datumsbasiert).ToString())
                {
                    Zufallsgeneratoren.Add(new Datumsbasiert());
                }
            }
            foreach(var generator in Zufallsgeneratoren)
            {
                generator.Verteilung = Verteilung == V.Gleichverteilung ? (Verteilung) new Gleichverteilung() : new Standardnormalverteilung(generator);
            }
        }

        private void VerfahrenKonvertieren(string[] a)
        {
            GüteTestVerfahren = new List<GüteTestverfahren>();
            foreach(var verfahren in a)
            {
                if(verfahren == ((int)GüteTestverfahren.SerielleAutokorrelation).ToString())
                {
                    GüteTestVerfahren.Add(GüteTestverfahren.SerielleAutokorrelation);
                }
                if (verfahren == ((int)GüteTestverfahren.SequenzUpDown).ToString())
                {
                    GüteTestVerfahren.Add(GüteTestverfahren.SequenzUpDown);
                }
                if (verfahren == ((int)GüteTestverfahren.Eigen).ToString())
                {
                    GüteTestVerfahren.Add(GüteTestverfahren.Eigen);
                }
            }
        }
    }
}
