using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main()
        {
            Day13Part2();
        }

        private static void Day1Part1()
        {
            var values = File.ReadAllLines("Day1Input.txt").Select(Int32.Parse);
            var fuel = values.Sum(Day1.CalculateFuel);
            Console.WriteLine(fuel);
        }

        private static void Day1Part2()
        {
            var values = File.ReadAllLines("Day1Input.txt").Select(Int32.Parse);
            var fuel = values.Sum(Day1.CalculateTotalFuel);
            Console.WriteLine(fuel);
        }

        private static void Day2Part1()
        {
            var source = File.ReadAllText("Day2Input.txt");
            var program = source.Split(',').Select(Int32.Parse).ToArray();
            program[1] = 12;
            program[2] = 2;
            Day2.IntcodeInterpreter(program);
            Console.WriteLine(program[0]);
        }

        private static void Day2Part2()
        {
            var source = File.ReadAllText("Day2Input.txt");
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var program = source.Split(',').Select(Int32.Parse).ToArray();
                    program[1] = noun;
                    program[2] = verb;
                    Day2.IntcodeInterpreter(program);
                    if (program[0] == 19690720)
                    {
                        Console.WriteLine(noun.ToString("D2") + verb.ToString("D2"));
                        return;
                    }
                }
            }
            Console.WriteLine("Error: No values found.");
        }

        private static void Day3Part1()
        {
            string[] paths = File.ReadAllLines("Day3Input.txt");
            Console.WriteLine(Day3.CalculateNearestIntersection(paths[0], paths[1]));
        }

        private static void Day3Part2()
        {
            string[] paths = File.ReadAllLines("Day3Input.txt");
            Console.WriteLine(Day3.CalculateQuickestIntersection(paths[0], paths[1]));
        }

        private static void Day4Part1()
        {
            int min = 240920;
            int max = 789857;
            Console.WriteLine(Enumerable.Range(min, max - min).Count(Day4.IsPossiblePassword));
        }

        private static void Day4Part2()
        {
            int min = 240920;
            int max = 789857;
            Console.WriteLine(Enumerable.Range(min, max - min).Count(Day4.IsPossiblePasswordStrict));
        }

        private static void Day5Part1()
        {
            BigInteger[] program = File.ReadAllText("Day5Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var computer = new IntcodeComputer(program);
            computer.IntcodeComputerInput += (s, e) => e.Value = 1;
            computer.IntcodeComputerOutput += (s, e) => Console.WriteLine(e.Value);
            computer.Run();
        }

        private static void Day5Part2()
        {
            BigInteger[] program = File.ReadAllText("Day5Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var computer = new IntcodeComputer(program);
            computer.IntcodeComputerInput += (s, e) => e.Value = 5;
            computer.IntcodeComputerOutput += (s, e) => Console.WriteLine(e.Value);
            computer.Run();
        }

        private static void Day6Part1()
        {
            string[] map = File.ReadAllLines("Day6Input.txt");
            Console.WriteLine(Day6.CountOrbits(map));
        }

        private static void Day6Part2()
        {
            string[] map = File.ReadAllLines("Day6Input.txt");
            Console.WriteLine(Day6.MinimumOrbitTransfers(map, "YOU", "SAN"));
        }

        private static void Day7Part1()
        {
            BigInteger[] program = File.ReadAllText("Day7Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            Console.WriteLine(Day7.FindMaxThrusterSignal(program));
        }

        private static void Day7Part2()
        {
            BigInteger[] program = File.ReadAllText("Day7Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            Console.WriteLine(Day7.FindMaxThrusterSignalFeedbackLoop(program));
        }

        private static void Day8Part1()
        {
            var image = new Day8.SpaceImage(25, 6, File.ReadAllText("Day8Input.txt"));
            var layers = image.Layers.Select(x => x.Data.Cast<int>());
            int[] zeroes = layers.Select(x => x.Count(y => y == 0)).ToArray();
            int minZeroesIndex = Array.IndexOf(zeroes, zeroes.Min());
            var minZeroLayer = layers.ElementAt(minZeroesIndex);
            int check = minZeroLayer.Count(x => x == 1) * minZeroLayer.Count(x => x == 2);
            Console.WriteLine(check);
        }

        private static void Day8Part2()
        {
            var image = new Day8.SpaceImage(25, 6, File.ReadAllText("Day8Input.txt"));
            image.Show();
        }

        private static void Day9Part1()
        {
            BigInteger[] program = File.ReadAllText("Day9Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var computer = new IntcodeComputer(program);
            computer.IntcodeComputerOutput += (s, e) => Console.WriteLine(e.Value);
            computer.Run();
            computer.Continue(1);
        }

        private static void Day9Part2()
        {
            BigInteger[] program = File.ReadAllText("Day9Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var computer = new IntcodeComputer(program);
            computer.IntcodeComputerOutput += (s, e) => Console.WriteLine(e.Value);
            computer.Run();
            computer.Continue(2);
        }

        private static void Day10Part1()
        {
            var map = File.ReadAllLines("Day10Input.txt");
            Console.WriteLine(Day10.BestAsteroidRank(map));
        }

        private static void Day10Part2()
        {
            var map = Day10.MapToList(File.ReadAllLines("Day10Input.txt"));
            var vaporized = Day10.VaporizeAsteroids(map, Day10.FindBestAsteroid(map)).ToList()[199];
            Console.WriteLine(vaporized.X * 100 + vaporized.Y);
        }

        private static void Day11Part1()
        {
            BigInteger[] program = File.ReadAllText("Day11Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var painted = Day11.PaintTiles(program);
            Console.WriteLine(painted.Count());
        }

        private static void Day11Part2()
        {
            BigInteger[] program = File.ReadAllText("Day11Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var painted = Day11.PaintTiles(program, 1);

            Console.WriteLine("Fullscreen...");
            Console.ReadKey(true);

            int minX = painted.Min(x => x.Key.X);
            int maxX = painted.Max(x => x.Key.X);
            int minY = painted.Min(x => x.Key.Y);
            int maxY = painted.Max(x => x.Key.Y);
            int width = maxX - minX + 1;
            int height = maxY - minY + 1;

            Console.Clear();
            Console.CursorVisible = false;
            foreach (var point in painted.Keys.Where(x => painted[x] == 1))
            {
                var translated = new Point(point.X - minX, point.Y - minY);
                Console.CursorLeft = 2 * translated.X;
                Console.CursorTop = height - translated.Y;
                Console.Write("##");
            }
            Console.ReadKey(true);
        }

        private static void Day12Part1()
        {
            var system = new Day12.GravitySystem(new Day12.Body[]
            {
                new Day12.Body(new Day12.Point3D(14, 15, -2)),
                new Day12.Body(new Day12.Point3D(17, -3, 4)),
                new Day12.Body(new Day12.Point3D(6, 12, -13)),
                new Day12.Body(new Day12.Point3D(-2, 10, -8)),
            });

            for (int i = 0; i < 1000; i++)
            {
                system.Update();
            }

            Console.WriteLine(system.Energy);
        }

        private static void Day12Part2()
        {
            var system = new Day12.GravitySystem(new Day12.Body[]
            {
                new Day12.Body(new Day12.Point3D(14, 15, -2)),
                new Day12.Body(new Day12.Point3D(17, -3, 4)),
                new Day12.Body(new Day12.Point3D(6, 12, -13)),
                new Day12.Body(new Day12.Point3D(-2, 10, -8)),
            });

            Console.WriteLine(system.CalculatePeriod());
        }

        private static void Day13Part1()
        {
            BigInteger[] program = File.ReadAllText("Day13Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            var game = new Day13.Game(program);
            game.Run();
            Console.WriteLine(game.Screen.Values.Count(x => x == Day13.GameTile.Block));
        }

        private static void Day13Part2()
        {
            BigInteger[] program = File.ReadAllText("Day13Input.txt").Split(',').Select(BigInteger.Parse).ToArray();
            program[0] = 2;
            var game = new Day13.Game(program);
            game.AutoRun();
        }
    }
}
