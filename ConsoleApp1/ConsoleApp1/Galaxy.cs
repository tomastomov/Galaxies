using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Galaxy : INamedSpaceObject
    {
        public Galaxy(GalaxyType galaxyType, string age, string name)
        {
            GalaxyType = galaxyType;
            Age = age;
            Name = name;
            Stars = new List<Star>();
        }

        public GalaxyType GalaxyType { get; private set; }

        public string Age { get; private set; }

        public string Name { get; }

        public ICollection<Star> Stars { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Type: {GalaxyType}");
            stringBuilder.AppendLine($"Age {Age}");
            if (Stars.Count > 0)
            {
                stringBuilder.AppendLine($"Stars: ");
                foreach (var star in Stars)
                {
                    stringBuilder.AppendLine(star.ToString());
                }
            }
            

            return stringBuilder.ToString();
        }
    }
}
