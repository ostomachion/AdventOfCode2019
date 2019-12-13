using NUnit.Framework;
using AdventOfCode2019;
using System;
using System.Numerics;

namespace AdventOfCode2019Tests
{
    public class Day5Tests
    {
        [Test]
        public void IntcodeComputerOriginalTest()
        {
            var computer = Day5.IntcodeInterpreter("1,9,10,3,2,3,11,0,99,30,40,50");
            Assert.AreEqual("3500,9,10,70,2,3,11,0,99,30,40,50", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);

            computer = Day5.IntcodeInterpreter("1,0,0,0,99");
            Assert.AreEqual("2,0,0,0,99", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);

            computer = Day5.IntcodeInterpreter("2,3,0,3,99");
            Assert.AreEqual("2,3,0,6,99", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);

            computer = Day5.IntcodeInterpreter("2,4,4,5,99,0");
            Assert.AreEqual("2,4,4,5,99,9801", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);

            computer = Day5.IntcodeInterpreter("1,1,1,4,99,5,6,0,99");
            Assert.AreEqual("30,1,1,4,2,5,6,0,99", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);
        }

        [Test]
        public void IntcodeComputerModeTest()
        {
            var computer = Day5.IntcodeInterpreter("1002,4,3,4,33");
            Assert.AreEqual("1002,4,3,4,99", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);
        }

        [Test]
        public void IntcodeComputerNegativeTest()
        {
            var computer = Day5.IntcodeInterpreter("1101,100,-1,4,0");
            Assert.AreEqual("1101,100,-1,4,99", computer.Memory.ToString());
            Assert.IsEmpty(computer.Outputs);
        }

        [Test]
        public void IntcodeComputerTest1()
        {
            string program = "3,9,8,9,10,9,4,9,99,-1,8";

            var computer = Day5.IntcodeInterpreter(program, 7);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 8);
            Assert.AreEqual(1, computer.Inputs.Count);
            Assert.AreEqual((BigInteger)8, computer.Inputs[0]);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 9);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);
        }

        [Test]
        public void IntcodeComputerTest2()
        {
            string program = "3,9,7,9,10,9,4,9,99,-1,8";

            var computer = Day5.IntcodeInterpreter(program, 7);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 8);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 9);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);
        }

        [Test]
        public void IntcodeComputerTest3()
        {
            string program = "3,3,1108,-1,8,3,4,3,99";

            var computer = Day5.IntcodeInterpreter(program, 7);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 8);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 9);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);
        }

        [Test]
        public void IntcodeComputerTest4()
        {
            string program = "3,3,1107,-1,8,3,4,3,99";

            var computer = Day5.IntcodeInterpreter(program, 7);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 8);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 9);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);
        }

        [Test]
        public void IntcodeComputerTest5()
        {
            string program = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";

            var computer = Day5.IntcodeInterpreter(program, 0);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 1);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 2);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);
        }

        [Test]
        public void IntcodeComputerTest6()
        {
            string program = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";

            var computer = Day5.IntcodeInterpreter(program, 0);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)0, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 1);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 2);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1, computer.Outputs[0]);
        }

        [Test]
        public void IntcodeComputerTest7()
        {
            string program = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";

            var computer = Day5.IntcodeInterpreter(program, 7);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)999, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 8);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1000, computer.Outputs[0]);

            computer = Day5.IntcodeInterpreter(program, 9);
            Assert.AreEqual(1, computer.Outputs.Count);
            Assert.AreEqual((BigInteger)1001, computer.Outputs[0]);
        }
    }
}