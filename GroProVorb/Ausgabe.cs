using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GroProVorb
{
    public class Ausgabe
    {
        string pfad;
        public Ausgabe(string pfad)
        {
            this.pfad = pfad;
        }

        public void Schreiben(string[] ergebnisse)
        {
            try
            {
                using (var writer = new StreamWriter(pfad))
                {
                    foreach(var ergebniss in ergebnisse)
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
