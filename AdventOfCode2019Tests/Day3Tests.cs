using NUnit.Framework;
using AdventOfCode2019;

namespace AdventOfCode2019Tests
{
    public class Day3Tests
    {
        [Test]
        public void CalculateNearestIntersectionTest()
        {
            Assert.AreEqual(6, Day3.CalculateNearestIntersection("R8,U5,L5,D3", "U7,R6,D4,L4"));
            Assert.AreEqual(159, Day3.CalculateNearestIntersection("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83"));
            Assert.AreEqual(135, Day3.CalculateNearestIntersection("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"));
        }

        [Test]
        public void CalculateQuickestIntersectionTest()
        {
            Assert.AreEqual(30, Day3.CalculateQuickestIntersection("R8,U5,L5,D3", "U7,R6,D4,L4"));
            Assert.AreEqual(610, Day3.CalculateQuickestIntersection("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83"));
            Assert.AreEqual(410, Day3.CalculateQuickestIntersection("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"));
        }
    }
}