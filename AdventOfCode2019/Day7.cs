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
    // https://adventofcode.com/2019/day/7
    public static class Day7
    {
        public static BigInteger FindMaxThrusterSignal(BigInteger[] program)
        {
            BigInteger value = 0;
            foreach (IEnumerable<int> settings in permute(new[] { 0, 1, 2, 3, 4 }))
            {
                BigInteger signal = 0;
                foreach (int phase in settings)
                {
                    var computer = new IntcodeComputer(program);
                    computer.Run();
                    computer.Continue(phase);
                    computer.Continue(signal);
                    signal = computer.Outputs.Single();
                }
                value = BigInteger.Max(value, signal);
            }
            return value;
        }

        public static BigInteger FindMaxThrusterSignalFeedbackLoop(BigInteger[] program)
        {
            BigInteger value = 0;
            foreach (IEnumerable<int> settings in permute(new[] { 5, 6, 7, 8, 9 }))
            {
                IntcodeComputer[] amps = new IntcodeComputer[5];
                for (int i = 0; i < amps.Length; i++)
                {
                    amps[i] = new IntcodeComputer(program);
                    amps[i].Run();
                    int phase = settings.ElementAt(i);
                    amps[i].Continue(settings.ElementAt(i));
                }

                int amp = 0;
                BigInteger signal = 0;
                int cycle = 0;
                while (!amps[4].Halted)
                {
                    amps[amp].Continue(signal);
                    if (amps[amp].Outputs.Count != cycle + 1) ;
                    signal = amps[amp].Outputs[cycle];
                    amp = (amp + 1) % 5;
                    if (amp == 0)
                        cycle++;
                }

                value = BigInteger.Max(value, amps[4].Outputs.Last());
            }
            return value;
        }

        private static IEnumerable<IEnumerable<int>> permute(IEnumerable<int> input)
        {
            if (!input.Any())
                yield return Enumerable.Empty<int>();

            for (int i = 0; i < input.Count(); i++)
            {
                foreach (IEnumerable<int> tail in permute(input.Take(i).Concat(input.Skip(i + 1))))
                {
                    yield return new List<int> { input.ElementAt(i) }.Concat(tail);
                }
            }
        }
    }
}
