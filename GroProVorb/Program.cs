using System;
using System.IO;

namespace GroProVorb
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile("C:/Users/matse/source/repos/GroProVorb/GroProVorb/Testfile.txt");

            WriteFile();

            Console.ReadKey();
        }

        /// <summary>
        /// Liest die Datei Zeile für Zeile
        /// </summary>
        /// <param name="url">Gibt den Speicherort der Datei an</param>
        /// <returns></returns>
        public static bool ReadFile(string url)
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

        /// <summary>
        /// Schreibt eine Datei auf den Desktop des Benutzers.
        /// </summary>
        public static void WriteFile()
        {
            // Create a string array with the lines of text
            string[] lines = { "First line", "Second line", "Third line" };

            // Set a variable to the Desktop path.
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
        }
    }
}
