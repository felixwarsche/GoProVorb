using System;
using System.IO;

namespace GroProVorb
{
    class Program
    {
        static void Main(string[] args)
        {
            //Einlesen der Daten
            Input input = new Input();
            bool couldReadFile = input.ReadFile("C:/Users/matse/source/repos/GroProVorb/GroProVorb/Testfile.txt");

            //Ausgeben der Daten
            Output output = new Output();
            output.WriteFile();

            Console.ReadKey();
        }
    }
}
