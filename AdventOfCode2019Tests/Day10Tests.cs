using NUnit.Framework;
using AdventOfCode2019;
using System;
using System.Numerics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019Tests
{
    public class Day10Tests
    {
        [Test]
        public void FindBestAsteroidTest()
        {
            string[] input = new[] {
                "##",
            };
            Assert.AreEqual(1, Day10.BestAsteroidRank(input));

            input = new[] {
                ".#..#",
                ".....",
                "#####",
                "....#",
                "...##",
            };
            Assert.AreEqual(8, Day10.BestAsteroidRank(input));

            input = new[]
            {
                "......#.#.",
                "#..#.#....",
                "..#######.",
                ".#.#.###..",
                ".#..#.....",
                "..#....#.#",
                "#..#....#.",
                ".##.#..###",
                "##...#..#.",
                ".#....####",
            };
            Assert.AreEqual(33, Day10.BestAsteroidRank(input));

            input = new[]
            {
                "#.#...#.#.",
                ".###....#.",
                ".#....#...",
                "##.#.#.#.#",
                "....#.#.#.",
                ".##..###.#",
                "..#...##..",
                "..##....##",
                "......#...",
                ".####.###.",
            };
            Assert.AreEqual(35, Day10.BestAsteroidRank(input));

            input = new[]
            {
                ".#..#..###",
                "####.###.#",
                "....###.#.",
                "..###.##.#",
                "##.##.#.#.",
                "....###..#",
                "..#.#..#.#",
                "#..#.#.###",
                ".##...##.#",
                ".....#.#..",
            };
            Assert.AreEqual(41, Day10.BestAsteroidRank(input));

            input = new[]
            {
                ".#..##.###...#######",
                "##.############..##.",
                ".#.######.########.#",
                ".###.#######.####.#.",
                "#####.##.#.##.###.##",
                "..#####..#.#########",
                "####################",
                "#.####....###.#.#.##",
                "##.#################",
                "#####.##.###..####..",
                "..######..##.#######",
                "####.##.####...##..#",
                ".#####..#.######.###",
                "##...#.##########...",
                "#.##########.#######",
                ".####.#.###.###.#.##",
                "....##.##.###..#####",
                ".#.#.###########.###",
                "#.#.#.#####.####.###",
                "###.##.####.##.#..##",
            };
            Assert.AreEqual(210, Day10.BestAsteroidRank(input));
        }

        [Test]
        public void VaporizeAsteroidsTest()
        {
            string[] input = new[]
            {
                ".#..##.###...#######",
                "##.############..##.",
                ".#.######.########.#",
                ".###.#######.####.#.",
                "#####.##.#.##.###.##",
                "..#####..#.#########",
                "####################",
                "#.####....###.#.#.##",
                "##.#################",
                "#####.##.###..####..",
                "..######..##.#######",
                "####.##.####...##..#",
                ".#####..#.######.###",
                "##...#.##########...",
                "#.##########.#######",
                ".####.#.###.###.#.##",
                "....##.##.###..#####",
                ".#.#.###########.###",
                "#.#.#.#####.####.###",
                "###.##.####.##.#..##",
            };
            Point laser = new Point(11, 13);

            List<Point> vaporized = Day10.VaporizeAsteroids(Day10.MapToList(input), laser).ToList();

            Assert.AreEqual(new Point(11, 12), vaporized[0]);
            Assert.AreEqual(new Point(12, 1), vaporized[1]);
            Assert.AreEqual(new Point(12, 2), vaporized[2]);
            Assert.AreEqual(new Point(12, 8), vaporized[9]);
            Assert.AreEqual(new Point(16, 0), vaporized[19]);
            Assert.AreEqual(new Point(16, 9), vaporized[49]);
            Assert.AreEqual(new Point(10, 16), vaporized[99]);
            Assert.AreEqual(new Point(9, 6), vaporized[198]);
            Assert.AreEqual(new Point(8, 2), vaporized[199]);
            Assert.AreEqual(new Point(10, 9), vaporized[200]);
            Assert.AreEqual(new Point(11, 1), vaporized[298]);
        }
    }
}