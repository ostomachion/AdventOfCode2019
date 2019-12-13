using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    public struct Argument
    {
        public BigInteger Position { get; private set; }
        public BigInteger Value { get; private set; }

        public Argument(BigInteger n)
        {
            this.Position = -1;
            this.Value = n;
        }

        public Argument(IntcodeMemory memory, BigInteger position)
        {
            this.Position = position;
            this.Value = memory[this.Position];
        }
    }

    public class IntcodeComputerIOEventArgs : EventArgs
    {
        public BigInteger? Value { get; set; }

        public IntcodeComputerIOEventArgs(BigInteger? value) => this.Value = value;
    }

    public class IntcodeMemory
    {
        private Dictionary<BigInteger, BigInteger> value;

        public IntcodeMemory() => this.value = new Dictionary<BigInteger, BigInteger>();

        public BigInteger this[BigInteger index]
        {
            get => this.value.ContainsKey(index) ? this.value[index] : 0;
            set
            {
                if (this.value.ContainsKey(index))
                    this.value[index] = value;
                else
                    this.value.Add(index, value);
            }
        }

        public override string ToString()
        {
            StringBuilder value = new StringBuilder();

            for (int i = 0; i <= this.value.Keys.Max(); i++)
            {
                value.Append(this[i] + ",");
            }

            return value.ToString().TrimEnd(',');
        }
    }

    public class IntcodeComputer
    {
        public event EventHandler<IntcodeComputerIOEventArgs> IntcodeComputerInput;
        public event EventHandler<IntcodeComputerIOEventArgs> IntcodeComputerOutput;

        public IntcodeMemory Memory { get; }
        public List<BigInteger> Inputs = new List<BigInteger>();
        public List<BigInteger> Outputs = new List<BigInteger>();
        private BigInteger ip = 0;
        public bool Halted { get; private set; }
        public bool Idle { get; private set; }
        private BigInteger idlePosition = -1;
        private BigInteger relativeBase = 0;

        public IntcodeComputer(BigInteger[] program)
        {
            this.Memory = new IntcodeMemory();
            for (int i = 0; i < program.Length; i++)
            {
                this.Memory[i] = program[i];
            }
        }

        public void Run()
        {
            while (!this.Halted && !this.Idle)
                Step();
        }

        public void Continue(BigInteger input)
        {
            if (!this.Idle)
                throw new InvalidOperationException();

            this.Inputs.Add(input);
            this.Memory[idlePosition] = input;

            this.idlePosition = -1;
            this.Idle = false;

            Run();
        }

        private void Step()
        {
            int code = (int)(this.Memory[this.ip] % 100);
            switch (code)
            {
                case 1:
                    this.OpAdd();
                    break;
                case 2:
                    this.OpMultiply();
                    break;
                case 3:
                    this.OpInput();
                    break;
                case 4:
                    this.OpOutput();
                    break;
                case 5:
                    this.OpJumpIfTrue();
                    break;
                case 6:
                    this.OpJumpIfFalse();
                    break;
                case 7:
                    this.OpLessThan();
                    break;
                case 8:
                    this.OpEquals();
                    break;
                case 9:
                    this.OpAdjustRelativeBase();
                    break;
                case 99:
                    this.Halt();
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported opcode {code}.");
            }
        }

        public enum ArgumentMode { Position = 0, Immediate = 1, Relative = 2 }

        private Argument[] GetArguments(int n)
        {
            Argument[] args = new Argument[n];

            BigInteger modes = this.Memory[this.ip] / 100;
            for (int i = 0; i < n; i++)
            {
                this.ip++;
                BigInteger value = this.Memory[this.ip];
                ArgumentMode mode = (ArgumentMode)(int)(modes % 10);
                modes /= 10;
                args[i] = mode switch
                {
                    ArgumentMode.Position => new Argument(this.Memory, (int)value),
                    ArgumentMode.Immediate => new Argument(value),
                    ArgumentMode.Relative => new Argument(this.Memory, (int)(value + this.relativeBase)),
                    _ => throw new InvalidOperationException($"Unsupported mode {mode}."),
                };
            }

            this.ip++;
            return args;
        }

        // 01
        private void OpAdd()
        {
            Argument[] args = GetArguments(3);
            this.Memory[args[2].Position] = args[0].Value + args[1].Value;
        }

        // 02
        private void OpMultiply()
        {
            Argument[] args = GetArguments(3);
            this.Memory[args[2].Position] = args[0].Value * args[1].Value;
        }

        // 03
        private void OpInput()
        {
            Argument[] args = GetArguments(1);
            var e = new IntcodeComputerIOEventArgs(null);
            OnIntcodeComputerInput(e);
            if (e.Value.HasValue)
            {
                this.Inputs.Add(e.Value.Value);
                this.Memory[args[0].Position] = e.Value.Value;
            }
            else
            {
                this.Idle = true;
                this.idlePosition = args[0].Position;
            }
        }

        // 04
        private void OpOutput()
        {
            Argument[] args = GetArguments(1);
            BigInteger value = args[0].Value;
            this.Outputs.Add(value);
            OnIntcodeComputerOutput(new IntcodeComputerIOEventArgs(value));
        }

        // 05
        private void OpJumpIfTrue()
        {
            Argument[] args = GetArguments(2);
            if (args[0].Value != 0)
                this.ip = args[1].Value;
        }

        // 06
        private void OpJumpIfFalse()
        {
            Argument[] args = GetArguments(2);
            if (args[0].Value == 0)
                this.ip = args[1].Value;
        }

        // 07
        private void OpLessThan()
        {
            Argument[] args = GetArguments(3);
            int value = args[0].Value < args[1].Value ? 1 : 0;
            this.Memory[args[2].Position] = value;
        }

        // 08
        private void OpEquals()
        {
            Argument[] args = GetArguments(3);
            int value = args[0].Value == args[1].Value ? 1 : 0;
            this.Memory[args[2].Position] = value;
        }

        // 09
        private void OpAdjustRelativeBase()
        {
            Argument[] args = GetArguments(1);
            this.relativeBase += args[0].Value;
        }

        // 99
        private void Halt() => this.Halted = true;

        protected virtual void OnIntcodeComputerOutput(IntcodeComputerIOEventArgs e) => IntcodeComputerOutput?.Invoke(this, e);
        protected virtual void OnIntcodeComputerInput(IntcodeComputerIOEventArgs e) => IntcodeComputerInput?.Invoke(this, e);
    }
}
