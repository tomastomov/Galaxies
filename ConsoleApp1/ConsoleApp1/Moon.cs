using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Moon : INamedSpaceObject
    {
        public Moon(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return $"                  {Name}";
        }
    }
}
