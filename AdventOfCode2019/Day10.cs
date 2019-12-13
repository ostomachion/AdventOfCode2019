using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/10
    public static class Day10
    {
        public static IEnumerable<Point> VisibleAsteroids(List<Point> asteroids, Point eye)
        {
            foreach (Point a in asteroids.Except(new[] { eye }))
            {
                Point delta = new Point(a.X - eye.X, a.Y - eye.Y);
                int d = Gcd(Math.Abs(delta.X), Math.Abs(delta.Y));

                Point epsilon = new Point(delta.X / d, delta.Y / d);

                bool blocked = false;
                for (int i = 1; i < d; i++)
                {
                    Point test = new Point(eye.X + i * epsilon.X, eye.Y + i * epsilon.Y);
                    if (asteroids.Contains(test))
                    {
                        blocked = true;
                        break;
                    }
                }
                if (!blocked)
                    yield return a;
            }
        }

        public static List<Point> MapToList(string[] map)
        {
            List<Point> value = new List<Point>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '#')
                        value.Add(new Point(x, y));
                }
            }
            return value;
        }

        public static int BestAsteroidRank(string[] map)
        {
            List<Point> asteroids = MapToList(map);
            Point best = FindBestAsteroid(asteroids);
            return VisibleAsteroids(asteroids, best).Count();
        }

        public static Point FindBestAsteroid(List<Point> asteroids)
        {
            int max = 0;
            Point value = asteroids[0];
            foreach (Point a in asteroids)
            {
                int count = VisibleAsteroids(asteroids, a).Count();
                if (count > max)
                {
                    value = a;
                    max = count;
                }
            }
            return value;
        }

        public static IEnumerable<Point> VaporizeAsteroids(List<Point> asteroids, Point laser)
        {
            while (asteroids.Count > 1)
            {
                foreach (var vaporized in VisibleAsteroids(asteroids, laser).OrderBy(a => (Math.Atan2(a.X - laser.X, laser.Y - a.Y) + 2 * Math.PI) % (2 * Math.PI)))
                {
                    asteroids.Remove(vaporized);
                    yield return vaporized;
                }
            }
        }

        private static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }
    }
}
