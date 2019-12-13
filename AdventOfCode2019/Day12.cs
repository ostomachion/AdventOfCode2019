using System;
using System.Collections.Generic;
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
    // https://adventofcode.com/2019/day/12
    public static class Day12
    {
        public class GravitySystem
        {
            private readonly List<Body> initialBodies;
            private readonly List<Body> bodies;

            public IEnumerable<Body> Bodies => this.bodies;

            public int Energy => this.bodies.Sum(x => x.Energy);

            public GravitySystem(IEnumerable<Body> bodies)
            {
                this.bodies = bodies.ToList();
                this.initialBodies = bodies.Select(x => x.Clone()).ToList();
            }

            public void Update()
            {
                ApplyGravity();
                UpdatePositions();
            }

            private void UpdatePositions()
            {
                foreach (var body in this.bodies)
                {
                    body.Update();
                }
            }

            private void ApplyGravity()
            {
                foreach (var body1 in this.bodies)
                {
                    foreach (var body2 in this.bodies)
                    {
                        var (x1, y1, z1) = body1.Position;
                        var (x2, y2, z2) = body2.Position;

                        int dx = x1 < x2 ? 1 : x1 > x2 ? -1 : 0;
                        int dy = y1 < y2 ? 1 : y1 > y2 ? -1 : 0;
                        int dz = z1 < z2 ? 1 : z1 > z2 ? -1 : 0;

                        body1.Velocity += new Point3D(dx, dy, dz);
                    }
                }
            }

            public long CalculatePeriod()
            {
                Point3D period = Point3D.Empty;

                Body[] periods = new Body[this.bodies.Count];
                for (int i = 0; i < periods.Length; i++)
                {
                    periods[i] = new Body(Point3D.Empty);
                }

                int t = 0;
                while (period.X == 0 || period.Y == 0 || period.Z == 0)
                {
                    this.Update();
                    t++;

                    if (period.X == 0)
                    {
                        bool same = true;
                        for (int i = 0; i < this.bodies.Count; i++)
                        {
                            var body = this.bodies[i];
                            var init = this.initialBodies[i];
                            if (body.Position.X != init.Position.X || body.Velocity.X != init.Velocity.X)
                            {
                                same = false;
                                break;
                            }
                        }
                        if (same)
                        {
                            period = new Point3D(t, period.Y, period.Z);
                        }
                    }

                    if (period.Y == 0)
                    {
                        bool same = true;
                        for (int i = 0; i < this.bodies.Count; i++)
                        {
                            var body = this.bodies[i];
                            var init = this.initialBodies[i];
                            if (body.Position.Y != init.Position.Y || body.Velocity.Y != init.Velocity.Y)
                            {
                                same = false;
                                break;
                            }
                        }
                        if (same)
                        {
                            period = new Point3D(period.X, t, period.Z);
                        }
                    }

                    if (period.Z == 0)
                    {
                        bool same = true;
                        for (int i = 0; i < this.bodies.Count; i++)
                        {
                            var body = this.bodies[i];
                            var init = this.initialBodies[i];
                            if (body.Position.Z != init.Position.Z || body.Velocity.Z != init.Velocity.Z)
                            {
                                same = false;
                                break;
                            }
                        }
                        if (same)
                        {
                            period = new Point3D(period.X, period.Y, t);
                        }
                    }
                }

                return Lcm(new long[] { period.X, period.Y, period.Z });
            }

            private static long Lcm(long[] numbers) => numbers.Aggregate(Lcm);
            private static long Lcm(long a, long b) => Math.Abs(a * b) / Gcd(a, b);
            private static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);
        }

        public class Body
        {
            public Point3D Position { get; set; }
            public Point3D Velocity { get; set; }

            public int Energy => this.Position.Energy * this.Velocity.Energy;

            public Body(Point3D position)
            {
                this.Position = position;
                this.Velocity = Point3D.Empty;
            }

            public Body(Point3D position, Point3D velocity)
            {
                this.Position = position;
                this.Velocity = velocity;
            }

            public void Update()
            {
                this.Position += this.Velocity;
            }

            public Body Clone() => new Body(this.Position, this.Velocity);
        }

        public struct Point3D
        {
            public static readonly Point3D Empty = new Point3D(0, 0, 0);

            public int X { get; }
            public int Y { get; }
            public int Z { get; }

            public int Energy => Math.Abs(this.X) + Math.Abs(this.Y) + Math.Abs(this.Z);

            public Point3D(int x, int y, int z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public static Point3D operator +(Point3D left, Point3D right)
            {
                return new Point3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
            }

            public void Deconstruct(out int x, out int y, out int z)
            {
                x = this.X;
                y = this.Y;
                z = this.Z;
            }
        }
    }
}
