using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/1
    public static class Day1
    {
        // Part 1
        public static int CalculateFuel(int mass)
        {
            return Math.Max(0, mass / 3 - 2);
        }

        // Part 2
        public static int CalculateTotalFuel(int mass)
        {
            int fuel = CalculateFuel(mass);
            return fuel <= 0 ? 0 : fuel + CalculateTotalFuel(fuel);
        }
    }
}
