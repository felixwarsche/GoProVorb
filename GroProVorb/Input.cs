using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GroProVorb
{
    class Input
    {
        //Alle Attribute, in die gespeichert werden sollen

        /// <summary>
        /// Liest die Datei Zeile für Zeile
        /// </summary>
        /// <param name="url">Gibt den Speicherort der Datei an</param>
        /// <returns></returns>
        public bool ReadFile(string url)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(url))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
