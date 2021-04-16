using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a command");

            var galaxies = new List<Galaxy>();

            string command = Console.ReadLine();

            while (command != "exit")
            {
                var splitCommand = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                var commandType = splitCommand[0];
                var spaceTypeObject = splitCommand.Length > 1 ? splitCommand[1] : null;
                switch (commandType)
                {
                    case "add":
                        switch (spaceTypeObject)
                        {
                            case "galaxy":
                                string name;
                                if (splitCommand.Length > 5)
                                {
                                    name = GetName(splitCommand[2], splitCommand[3]);
                                    var type = Enum.Parse<GalaxyType>(splitCommand[4], true);
                                    string age = splitCommand[5];
                                    galaxies.Add(new Galaxy(type, age, name));
                                }
                                else
                                {
                                    name = GetName(splitCommand[2]);
                                    var type = Enum.Parse<GalaxyType>(splitCommand[3], true);
                                    string age = splitCommand[4];
                                    galaxies.Add(new Galaxy(type, age, name));
                                }
                            break;
                            case "star":
                                var galaxyName = GetName(splitCommand[2], splitCommand[3]);
                                var processedArguments = ProcessNameArgumentsWithSpacesAndBrackets(splitCommand);
                                var arguments = ExtractStarArguments(splitCommand, processedArguments.startingIndex);
                                galaxies.FirstOrDefault(galaxy => galaxy.Name == galaxyName).Stars.Add(new Star(processedArguments.name, arguments.mass, arguments.size, arguments.temperature, arguments.compatibility));
                                break;
                            case "planet":
                                var starName = GetName(splitCommand[2], splitCommand[3]);
                                var planetProcessedArgs = ProcessNameArgumentsWithSpacesAndBrackets(splitCommand);
                                var index = planetProcessedArgs.startingIndex;
                                var planetType = Enum.Parse<PlanetType>(splitCommand[index++], true);
                                var isInhabitated = splitCommand[index] == "yes" ? true : false;
                                galaxies.SelectMany(galaxy => galaxy.Stars).FirstOrDefault(star => star.Name == starName).Planets.Add(new Planet(planetProcessedArgs.name, isInhabitated, planetType));
                                break;
                            case "moon":
                                var planetName = GetName(splitCommand[2], splitCommand[3]);
                                var moonProcessedArgs = ProcessNameArgumentsWithSpacesAndBrackets(splitCommand);
                                galaxies.SelectMany(galaxy => galaxy.Stars).SelectMany(star => star.Planets).FirstOrDefault(planet => planet.Name == planetName).Moons.Add(new Moon(moonProcessedArgs.name));
                                break;
                            default:
                                break;
                        }
                        break;
                    case "list":
                        var listType = splitCommand[1];
                        Console.WriteLine($"--- List of all researched {listType} ---");
                        switch (listType)
                        {
                            case "galaxies":
                                Console.WriteLine(GetListedSpaceObject(galaxies));
                                break;
                            case "stars":
                                Console.WriteLine(GetListedSpaceObject(galaxies.SelectMany(galaxy => galaxy.Stars)));
                                break;
                            case "planets":
                                Console.WriteLine(GetListedSpaceObject(galaxies.SelectMany(galaxy => galaxy.Stars).SelectMany(stars => stars.Planets)));
                                break;
                            case "moons":
                                Console.WriteLine(GetListedSpaceObject(galaxies.SelectMany(galaxy => galaxy.Stars).SelectMany(stars => stars.Planets).SelectMany(planet => planet.Moons)));
                                break;
                            default:
                                break;
                        }
                        Console.WriteLine($"--- End of {listType} list ---");
                        break;
                    case "stats":
                        Console.WriteLine("--- Stats ---");
                        var stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine($"Galaxies: {galaxies.Count}");
                        stringBuilder.AppendLine($"Stars: {galaxies.SelectMany(galaxy => galaxy.Stars).Count()}");
                        stringBuilder.AppendLine($"Planets: {galaxies.SelectMany(galaxy => galaxy.Stars).SelectMany(stars => stars.Planets).Count()}");
                        stringBuilder.AppendLine($"Moons: {galaxies.SelectMany(galaxy => galaxy.Stars).SelectMany(stars => stars.Planets).SelectMany(planets => planets.Moons).Count()}");
                        Console.Write(stringBuilder.ToString());
                        Console.WriteLine("--- End of stats ---");
                        break;
                    case "print":
                        var galaxyResearchName = GetName(splitCommand[1], splitCommand.Length > 2 ? splitCommand[2] : null);
                        Console.WriteLine($"--- Data for {galaxyResearchName} galaxy ---");
                        Console.WriteLine(galaxies.FirstOrDefault(galaxy => galaxy.Name == galaxyResearchName));
                        Console.WriteLine($"--- End of data for {galaxyResearchName} galaxy ---");
                        break;
                    default:
                        break;
                }

                command = Console.ReadLine();
            }
        }

        static (string name, int startingIndex) ProcessNameArgumentsWithSpacesAndBrackets(string[] splitCommand)
        {
            string name = null;
            var startingIndex = 0;
            if (splitCommand[3].StartsWith('['))
            {
                if (splitCommand[3].EndsWith(']'))
                {
                    name = GetName(splitCommand[3]);
                    startingIndex = 4;
                }
                else
                {
                    name = GetName(splitCommand[3], splitCommand[4]);
                    startingIndex = 5;
                }
            }
            else
            {
                if (splitCommand[4].EndsWith(']'))
                {
                    name = GetName(splitCommand[4]);
                    startingIndex = 5;
                }
                else
                {
                    name = GetName(splitCommand[4], splitCommand[5]);
                    startingIndex = 6;
                }
            }

            return (name, startingIndex);
        }

        static (double mass, double size, int temperature, double compatibility) ExtractStarArguments(string[] arguments, int startingIndex)
        {
            var mass = double.Parse(arguments[startingIndex++]);
            var size = double.Parse(arguments[startingIndex++]);
            var temperature = int.Parse(arguments[startingIndex++]);
            var compatibility = double.Parse(arguments[startingIndex++]);

            return (mass, size, temperature, compatibility);
        }

        static string GetListedSpaceObject<T>(IEnumerable<T> spaceObjects) where T : INamedSpaceObject
        {
            return spaceObjects.Aggregate(new StringBuilder(), (sb, next) =>
            {
                sb.Append($"{next.Name}, ");
                return sb;
            }, sb => sb.ToString().TrimEnd(','));
        }

        static string GetName(string firstPart, string secondPart = null)
        {
            if (firstPart.StartsWith("[") && firstPart.EndsWith("]"))
            {
                return firstPart.Trim('[').Trim(']');
            }
            else if (firstPart.StartsWith("[") && secondPart.EndsWith("]"))
            {
                var newName = firstPart + " " + secondPart;
                return newName.Trim('[').Trim(']');
            }

            return null;
        }
    }
}
