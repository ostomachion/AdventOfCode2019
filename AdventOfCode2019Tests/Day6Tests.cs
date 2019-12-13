using NUnit.Framework;
using AdventOfCode2019;
using System;

namespace AdventOfCode2019Tests
{
    public class Day6Tests
    {
        [Test]
        public void CountOrbitsTest()
        {
            Assert.AreEqual(42, Day6.CountOrbits(new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" }));
        }

        [Test]
        public void MinimumOrbitTransfersTest()
        {
            Assert.AreEqual(4, Day6.MinimumOrbitTransfers(new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" }, "YOU", "SAN"));
        }
    }
}