using NUnit.Framework;
using AdventOfCode2019;

namespace AdventOfCode2019Tests
{
    public class Day1Tests
    {
        [Test]
        public void CalculateFuelTest()
        {
            Assert.AreEqual(2, Day1.CalculateFuel(12));
            Assert.AreEqual(2, Day1.CalculateFuel(14));
            Assert.AreEqual(654, Day1.CalculateFuel(1969));
            Assert.AreEqual(33583, Day1.CalculateFuel(100756));
        }

        [Test]
        public void CalculateTotalFuelTest()
        {
            Assert.AreEqual(2, Day1.CalculateTotalFuel(14));
            Assert.AreEqual(966, Day1.CalculateTotalFuel(1969));
            Assert.AreEqual(50346, Day1.CalculateTotalFuel(100756));
        }
    }
}