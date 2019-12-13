using NUnit.Framework;
using AdventOfCode2019;
using System;
using System.Numerics;

namespace AdventOfCode2019Tests
{
    public class Day9Tests
    {
        [Test]
        public void Test1()
        {
            var computer = new IntcodeComputer(new BigInteger[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 });
            computer.Run();

            Assert.AreEqual(new BigInteger[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 }, computer.Outputs.ToArray());
        }

        [Test]
        public void Test2()
        {
            var computer = new IntcodeComputer(new BigInteger[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 });
            computer.Run();

            Assert.AreEqual(new BigInteger[] { 1219070632396864 }, computer.Outputs.ToArray());
        }

        [Test]
        public void Test3()
        {
            var computer = new IntcodeComputer(new BigInteger[] { 104, 1125899906842624, 99 });
            computer.Run();

            Assert.AreEqual(new BigInteger[] { 1125899906842624 }, computer.Outputs.ToArray());
        }
    }
}