using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Star : INamedSpaceObject
    {
        public Star(string name, double mass, double size, int temperature, double luminosity)
        {
            Name = name;
            Mass = mass;
            Size = size;
            Temperature = temperature;
            Luminosity = luminosity;
            Planets = new List<Planet>();
        }

        public string Name { get; private set; }

        public double Mass { get; private set; }

        public double Size { get; private set; }

        public char Class => GetClass();

        public int Temperature { get; private set; }

        public double Luminosity { get; private set; }

        public ICollection<Planet> Planets { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"   -   Name: {Name}");
            stringBuilder.AppendLine($"       Class: {Class} ({Mass}, {Size}, {Temperature}, {Luminosity})");
            stringBuilder.AppendLine("       Planets:");
            if (Planets.Count > 0)
            {
                foreach (var planet in Planets)
                {
                    stringBuilder.AppendLine(planet.ToString());
                }
            }
            else
            {
                stringBuilder.AppendLine("            None");
            }
            return stringBuilder.ToString();
        }

        private char GetClass()
        {
            if (IsValid(30000, int.MaxValue, 30000, double.MaxValue, 16, double.MaxValue, 6.6, double.MaxValue))
            {
                return 'O';
            }
            else if (IsValid(10000, 30000, 25, 30000, 2.1, 16, 1.8, 6.6))
            {
                return 'B';
            }
            else if (IsValid(7500, 10000, 5, 25, 1.4, 2.1, 1.4, 1.8))
            {
                return 'A';
            }
            else if (IsValid(6000, 7500, 1.5, 5, 1.04, 1.4, 1.15, 1.4))
            {
                return 'F';
            }
            else if (IsValid(5200, 6000, 0.6, 1.5, 0.8, 1.04, 0.96, 1.15))
            {
                return 'G';
            }
            else if (IsValid(3700, 5200, 0.08, 0.6, 0.45, 0.8, 0.7, 0.96))
            {
                return 'K';
            }
            else if (IsValid(2400, 3700, double.MinValue, 0.08, 0.08, 0.45, double.MinValue, 0.7))
            {
                return 'M';
            }

            return ' ';
        }

        private bool IsValid(int lowerTempLimit, int upperTempLimit, double lowerCompatibilityLimit, double upperCompatibilityLimit, double lowerMassLimit, double upperMassLimit, double lowerSizeLimit, double upperSizeLimit)
        {
            return Temperature >= lowerTempLimit && Temperature < upperTempLimit && Luminosity >= lowerCompatibilityLimit && Luminosity < upperCompatibilityLimit && Mass >= lowerMassLimit && Mass < upperMassLimit && Size >= lowerSizeLimit && Size < upperSizeLimit;
        }
    }
}
