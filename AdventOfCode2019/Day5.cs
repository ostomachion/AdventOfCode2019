using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/5
    public static class Day5
    {
        public static IntcodeComputer IntcodeInterpreter(string source, params int[] input)
        {
            BigInteger[] program = source.Split(',').Select(BigInteger.Parse).ToArray();
            var computer = new IntcodeComputer(program);
            computer.IntcodeComputerInput += (s, e) => e.Value = input[computer.Inputs.Count];
            computer.Run();
            return computer;
        }
    }
}
