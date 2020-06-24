﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zufallsklassen
{
    public class LCG : Zufallsbibliothek
    {
        private Generatorenklasse Art;
        private double[] Parameter;

        public Verteilung Verteilung { get; set; } = new Gleichverteilung();

        public LCG(Generatorenklasse art ,double[] p)
        {
            this.Art = art;
            Parameter = p;
        }

        public LCG(Generatorenklasse art, double[] p, Verteilung v)
        {
            this.Art = art;
            Parameter = p;
            Verteilung = v;
        }

        /// <summary>
        /// Generiert eine Zufallszahl in Abhängikeit der gegebenen Parameter und speichert den xi-Parameter nach Schleifendurchlauf.
        /// 
        /// Es wird auch abgefragt, welche Verteilung hier angewendet werden soll und dann ggflls. transformiert.
        /// </summary>
        /// <returns>Zufallszahl mit gegebener Verteilung</returns>
        public double GeneriereZufallszahl()
        {
            double x = GeneriereGleichverteilteZufallszahl01();

            return Verteilung.Transformiere(x);
        }

        /// <summary>
        /// Berechnet eine Gleichverteilte Zufallszahl nach dem Prinzip der erhaltenen Parameter
        /// </summary>
        /// <returns>Zufallszahl</returns>
        public double GeneriereGleichverteilteZufallszahl01()
        {
            double m = Parameter[0]; double a = Parameter[1]; double c = Parameter[2]; double x = Parameter[3];
            double wertBisMudolo = ((a * x) + c) % m;
            double wert = (double)wertBisMudolo / m;
            Parameter[3] = wertBisMudolo;
            return wert;
        }

        public double GetM()
        {
            return this.Parameter[0];
        }

        public Generatorenklasse GetArt()
        {
            return this.Art;
        }
    }


    public class LCGVerfahrenParameter
    {
        public static readonly double[] AnsiC = new double[] { Math.Pow(2, 31), 1103515245, 12345, 12345 };
        public static readonly double[] MinimalStandard = new double[] { Math.Pow(2, 31) - 1, 16807, 0, 1 };
        public static readonly double[] RANDU = new double[] { Math.Pow(2, 31), 65539, 0, 1 };
        public static readonly double[] SIMSCRIPT = new double[] { Math.Pow(2, 31) - 1, 630360016, 0, 1 };
        public static readonly double[] NAGsLCG = new double[] { Math.Pow(2, 59), Math.Pow(13, 13), 0, 123456789 };
        public static readonly double[] MaplesLCG = new double[] { Math.Pow(10, 12) - 11, 427419669081, 0, 1 };
    }
}
