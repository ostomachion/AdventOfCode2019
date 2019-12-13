using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/13
    public static class Day13
    {
        public enum GameTile { Empty, Wall, Block, HorizontalPaddle, Ball }

        public class Game
        {
            private enum OutputState { X, Y, Update }
            private OutputState outputState = OutputState.X;
            private Point outputPoint = new Point();

            private Point paddle;
            private Point ball;

            private IntcodeComputer comptuer;

            private Dictionary<Point, GameTile> screen = new Dictionary<Point, GameTile>();
            public ReadOnlyDictionary<Point, GameTile> Screen => new ReadOnlyDictionary<Point, GameTile>(screen);
            public long Score { get; private set; }

            public Game(BigInteger[] program)
            {
                this.comptuer = new IntcodeComputer(program);
                this.comptuer.IntcodeComputerOutput += ComputerOutput;
            }

            public void Run()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.SetWindowSize(20, 10);
                Console.Clear();
                this.comptuer.Run();
                while (!this.comptuer.Halted)
                {
                    Console.CursorVisible = false;
                    Thread.Sleep(500);
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.LeftArrow)
                            this.comptuer.Continue(-1);
                        else if (key.Key == ConsoleKey.RightArrow)
                            this.comptuer.Continue(1);
                    }
                    else
                    {
                        this.comptuer.Continue(0);
                    }
                }
            }

            public void AutoRun()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.SetWindowSize(20, 10);
                Console.Clear();
                this.comptuer.Run();
                while (!this.comptuer.Halted)
                {
                    Thread.Sleep(30);
                    Console.CursorVisible = false;
                    if (ball.X < paddle.X)
                        this.comptuer.Continue(-1);
                    else if (ball.X > paddle.X)
                        this.comptuer.Continue(1);
                    else
                        this.comptuer.Continue(0);
                }
            }

            private void ComputerOutput(object sender, IntcodeComputerIOEventArgs e)
            {
                switch (this.outputState)
                {
                    case OutputState.X:
                        this.outputPoint.X = (int)e.Value;
                        this.outputState = OutputState.Y;
                        break;
                    case OutputState.Y:
                        this.outputPoint.Y = (int)e.Value;
                        this.outputState = OutputState.Update;
                        break;
                    case OutputState.Update:
                        if (this.outputPoint.X == -1 && this.outputPoint.Y == 0)
                        {
                            // Update score.
                            this.Score = (long)e.Value;
                            DrawScore();
                        }
                        else
                        {
                            this.SetTile(this.outputPoint, (GameTile)(int)e.Value);
                        }
                        this.outputState = OutputState.X;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            private void SetTile(Point p, GameTile tile)
            {
                if (tile == GameTile.HorizontalPaddle)
                    this.paddle = p;
                else if (tile == GameTile.Ball)
                    this.ball = p;

                if (this.screen.ContainsKey(p))
                    this.screen[p] = tile;
                else
                    this.screen.Add(p, tile);

                DrawTile(p);
            }

            private void DrawTile(Point p)
            {
                GameTile tile = this.screen[p];

                int x = p.X * 2;
                int y = p.Y + 1;

                if (Console.WindowWidth <= x)
                {
                    Console.WindowWidth = x + 2;
                    Redraw();
                }

                if (Console.WindowHeight <= y)
                {
                    Console.WindowHeight = y + 2;
                    Redraw();
                }

                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.ForegroundColor = tile switch
                {
                    GameTile.Empty => ConsoleColor.Black,
                    GameTile.Wall => ConsoleColor.DarkGray,
                    GameTile.Block => ConsoleColor.White,
                    GameTile.HorizontalPaddle => ConsoleColor.Green,
                    GameTile.Ball => ConsoleColor.Red,
                    _ => throw new InvalidOperationException()
                };
                Console.Write("██");
            }

            private void Redraw()
            {
                Console.Clear();
                DrawScore();
                foreach (var p in this.screen.Keys)
                    DrawTile(p);
            }

            private void DrawScore()
            {
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(this.Score.ToString().PadLeft(10, '0').PadLeft(Console.WindowWidth, ' '));
            }

            private GameTile GetTile(Point p)
            {
                return this.screen.ContainsKey(p) ? this.screen[p] : GameTile.Empty;
            }
        }
    }
}
