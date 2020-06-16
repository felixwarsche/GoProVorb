using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GroProVorb
{
    class Output
    {
        //Alle Attribute die nötig sind um zu schreiben. ggflls. reicht eine Dependency auf die Hauptklasse


        /// <summary>
        /// Schreibt eine Datei auf den Desktop des Benutzers.
        /// </summary>
        public void WriteFile()
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
