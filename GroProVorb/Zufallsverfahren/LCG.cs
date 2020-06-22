using System;
using System.Collections.Generic;
using System.Text;

namespace GroProVorb
{
    class LCG : Zufallsbibliothek
    {
        private string name;
        private long[] parameter;

        public LCG(long[] p)
        {
            parameter = p;
        }

        /// <summary>
        /// Generiert eine Zufallszahl in Abhängikeit der gegebenen Parameter und speichert den x-Parameter nach Schleifendurchlauf.
        /// </summary>
        /// <returns></returns>
        public double GeneriereZufallszahl()
        {
            long m = parameter[0];
            long a = parameter[1];
            long c = parameter[2];
            long x = parameter[3];
            long ret = ((a * x) + c) % m;
            parameter[3] = ret;
            return ret / m;
        }
    }


    public class LCGVerfahrenParameter
    {
        public long[] AnsiC = new long[] { (long)Math.Pow(2, 31), 1103515245, 12345, 12345 };
        public long[] MinimalStandard = new long[] { (long)Math.Pow(2, 31) - 1, 16807, 0, 1 };
        public long[] RANDU = new long[] { (long)Math.Pow(2, 31), 65539, 0, 1 };
        public long[] SIMSCRIPT = new long[] { (long)Math.Pow(2, 31) - 1, 630360016, 0, 1 };
        public long[] NAGsLCG = new long[] { (long)Math.Pow(2, 59), (long)Math.Pow(13, 13), 0, 123456789 };
        public long[] MaplesLCG = new long[] { (long)Math.Pow(10, 12) - 11, 427419669081, 0, 1 };
    }
}
