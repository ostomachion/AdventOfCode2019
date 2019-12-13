using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/2
    public static class Day2
    {
        public static string IntcodeInterpreter(string source)
        {
            int[] program = source.Split(',').Select(Int32.Parse).ToArray();
            IntcodeInterpreter(program);
            return String.Join(',', program);
        }

        public static void IntcodeInterpreter(int[] program)
        {
            int ip = 0;
            while (true)
            {
                switch(program[ip])
                {
                    case 1:
                    {
                        int left = program[ip + 1];
                        int right = program[ip + 2];
                        int output = program[ip + 3];
                        program[output] = program[left] + program[right];
                        ip += 4;
                        break;
                    }
                    case 2:
                    {
                        int left = program[ip + 1];
                        int right = program[ip + 2];
                        int output = program[ip + 3];
                        program[output] = program[left] * program[right];
                        ip += 4;
                        break;
                    }
                    case 99:
                        return;
                    default:
                        throw new InvalidOperationException($"Unexpected opcode {program[ip]}.");
                }
            }
        }
    }
}
