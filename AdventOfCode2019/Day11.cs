using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/11
    public static class Day11
    {
        public static Dictionary<Point, int> PaintTiles(BigInteger[] program, int startColor = 0)
        {
            Point current = new Point(0, 0);
            Point direction = new Point(0, 1);
            Dictionary<Point, int> paintedTiles = new Dictionary<Point, int> { [current] = startColor };
            var computer = new IntcodeComputer(program);
            int outputIndex = 0;
            while(!computer.Halted)
            {
                computer.Run();
                int input = paintedTiles.ContainsKey(current) ? paintedTiles[current] : 0;
                computer.Continue(input);
                if (!paintedTiles.ContainsKey(current))
                    paintedTiles.Add(current, 0);
                paintedTiles[current] = (int)computer.Outputs[outputIndex];
                direction = (int)computer.Outputs[outputIndex + 1] == 0 ? new Point(-direction.Y, direction.X) : new Point(direction.Y, -direction.X);
                current = new Point(current.X + direction.X, current.Y + direction.Y);
                outputIndex += 2;
            }
            return paintedTiles;
        }
    }
}
