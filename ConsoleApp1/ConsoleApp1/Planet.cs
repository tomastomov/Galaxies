using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Planet : INamedSpaceObject
    {
        public Planet(string name, bool inhabitated, PlanetType planetType)
        {
            Name = name;
            Inhabitated = inhabitated;
            PlanetType = planetType;
            Moons = new List<Moon>();
        }

        public string Name { get; private set; }

        public bool Inhabitated { get; private set; }

        public PlanetType PlanetType { get; private set; }

        public ICollection<Moon> Moons { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            var isInhabitated = Inhabitated ? "yes" : "no";

            stringBuilder.AppendLine($"           Name: {Name}");
            stringBuilder.AppendLine($"           Type: {PlanetType}");
            stringBuilder.AppendLine($"           Support Life: {isInhabitated}");

            if (Moons.Count > 0)
            {
                stringBuilder.AppendLine($"           Moons:");

                foreach (var moon in Moons)
                {
                    stringBuilder.AppendLine(moon.ToString());
                }

            }

            return stringBuilder.ToString();
        }
    }
}
