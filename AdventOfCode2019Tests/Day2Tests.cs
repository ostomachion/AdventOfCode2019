using NUnit.Framework;
using AdventOfCode2019;

namespace AdventOfCode2019Tests
{
    public class Day2Tests
    {
        [Test]
        public void IntcodeInterpreterTest()
        {
            Assert.AreEqual("3500,9,10,70,2,3,11,0,99,30,40,50", Day2.IntcodeInterpreter("1,9,10,3,2,3,11,0,99,30,40,50"));
            Assert.AreEqual("2,0,0,0,99", Day2.IntcodeInterpreter("1,0,0,0,99"));
            Assert.AreEqual("2,3,0,6,99", Day2.IntcodeInterpreter("2,3,0,3,99"));
            Assert.AreEqual("2,4,4,5,99,9801", Day2.IntcodeInterpreter("2,4,4,5,99,0"));
            Assert.AreEqual("30,1,1,4,2,5,6,0,99", Day2.IntcodeInterpreter("1,1,1,4,99,5,6,0,99"));
        }
    }
}