using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/3
    public static class Day3
    {
        // Part 1
        public static int CalculateNearestIntersection(string path1, string path2)
        {
            HashSet<Point> commonPoints = getPointsFromPath(path1);
            commonPoints.IntersectWith(getPointsFromPath(path2));

            return commonPoints.Min(p => Math.Abs(p.X) + Math.Abs(p.Y));

            static HashSet<Point> getPointsFromPath(string path)
            {
                HashSet<Point> value = new HashSet<Point>();

                Point current = new Point(0, 0);
                foreach (string instruction in path.Split(','))
                {
                    int distance = Int32.Parse(instruction[1..]);
                    (int dx, int dy) = instruction[0] switch
                    {
                        'U' => (0, 1),
                        'D' => (0, -1),
                        'R' => (1, 0),
                        'L' => (-1, 0),
                        _ => throw new Exception("Invalid direction.")
                    };

                    for (int i = 0; i < distance; i++)
                    {
                        current.Offset(dx, dy);
                        value.Add(current);
                    }
                }

                return value;
            }
        }

        // Part 2
        public static int CalculateQuickestIntersection(string path1, string path2)
        {
            Dictionary<Point, int> points1 = getPointsFromPath(path1);
            Dictionary<Point, int> points2 = getPointsFromPath(path2);

            var commonPoints = points1.Keys.Intersect(points2.Keys);

            return commonPoints.Min(p => points1[p] + points2[p]);

            static Dictionary<Point, int> getPointsFromPath(string path)
            {
                Dictionary<Point, int> value = new Dictionary<Point, int>();

                Point current = new Point(0, 0);
                int time = 0;
                foreach (string instruction in path.Split(','))
                {
                    int distance = Int32.Parse(instruction[1..]);
                    (int dx, int dy) = instruction[0] switch
                    {
                        'U' => (0, 1),
                        'D' => (0, -1),
                        'R' => (1, 0),
                        'L' => (-1, 0),
                        _ => throw new Exception("Invalid direction.")
                    };

                    for (int i = 0; i < distance; i++)
                    {
                        time++;
                        current.Offset(dx, dy);
                        if (!value.ContainsKey(current))
                            value.Add(current, time);
                    }
                }

                return value;
            }
        }
    }
}
