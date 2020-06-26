using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GroProVorb
{
    public class Ausgabe
    {
        string Pfad;
        public Ausgabe(string pfad)
        {
            this.Pfad = pfad;
        }

        public void Schreiben(string[] ergebnisse)
        {
            try
            {
                using (var writer = new StreamWriter(Pfad))
                {
                    foreach (var ergebniss in ergebnisse)
                    {
                        writer.WriteLine(ergebniss);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Die Datei konnte nicht gefunden werden. E: " + e);
            }
        }

    }
}
