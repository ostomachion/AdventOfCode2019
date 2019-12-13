using NUnit.Framework;
using AdventOfCode2019;
using System;
using System.Numerics;
using static AdventOfCode2019.Day12;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019Tests
{
    public class Day12Tests
    {
        [Test]
        public void ApplyGravityTest()
        {
            var system = new GravitySystem(new Body[]
            {
                new Body(new Point3D(-1, 0, 2)),
                new Body(new Point3D(2, -10, -7)),
                new Body(new Point3D(4, -8, 8)),
                new Body(new Point3D(3, 5, -1)),
            });

            // After 0 steps.
            checkSystem(new[]
            {
                (new Point3D(-1, 0, 2), new Point3D(0, 0, 0)),
                (new Point3D(2, -10, -7), new Point3D(0, 0, 0)),
                (new Point3D(4, -8, 8), new Point3D(0, 0, 0)),
                (new Point3D(3, 5, -1), new Point3D(0, 0, 0)),
            }, system);

            // After 1 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(2, -1, 1), new Point3D(3, -1, -1)),
                (new Point3D(3, -7, -4), new Point3D(1, 3, 3)),
                (new Point3D(1, -7, 5), new Point3D(-3, 1, -3)),
                (new Point3D(2, 2, 0), new Point3D(-1, -3, 1)),
            }, system);

            // After 2 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(5, -3, -1), new Point3D(3, -2, -2)),
                (new Point3D(1, -2, 2), new Point3D(-2, 5, 6)),
                (new Point3D(1, -4, -1), new Point3D(0, 3, -6)),
                (new Point3D(1, -4, 2), new Point3D(-1, -6, 2)),
            }, system);

            // After 3 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(5, -6, -1), new Point3D(0, -3, 0)),
                (new Point3D(0, 0, 6), new Point3D(-1, 2, 4)),
                (new Point3D(2, 1, -5), new Point3D(1, 5, -4)),
                (new Point3D(1, -8, 2), new Point3D(0, -4, 0)),
            }, system);

            // After 4 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(2, -8, 0), new Point3D(-3, -2, 1)),
                (new Point3D(2, 1, 7), new Point3D(2, 1, 1)),
                (new Point3D(2, 3, -6), new Point3D(0, 2, -1)),
                (new Point3D(2, -9, 1), new Point3D(1, -1, -1)),
            }, system);

            // After 5 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(-1, -9, 2), new Point3D(-3, -1, 2)),
                (new Point3D(4, 1, 5), new Point3D(2, 0, -2)),
                (new Point3D(2, 2, -4), new Point3D(0, -1, 2)),
                (new Point3D(3, -7, -1), new Point3D(1, 2, -2)),
            }, system);

            // After 6 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(-1, -7, 3), new Point3D(0, 2, 1)),
                (new Point3D(3, 0, 0), new Point3D(-1, -1, -5)),
                (new Point3D(3, -2, 1), new Point3D(1, -4, 5)),
                (new Point3D(3, -4, -2), new Point3D(0, 3, -1)),
            }, system);

            // After 7 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(2, -2, 1), new Point3D(3, 5, -2)),
                (new Point3D(1, -4, -4), new Point3D(-2, -4, -4)),
                (new Point3D(3, -7, 5), new Point3D(0, -5, 4)),
                (new Point3D(2, 0, 0), new Point3D(-1, 4, 2)),
            }, system);

            // After 8 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(5, 2, -2), new Point3D(3, 4, -3)),
                (new Point3D(2, -7, -5), new Point3D(1, -3, -1)),
                (new Point3D(0, -9, 6), new Point3D(-3, -2, 1)),
                (new Point3D(1, 1, 3), new Point3D(-1, 1, 3)),
            }, system);

            // After 9 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(5, 3, -4), new Point3D(0, 1, -2)),
                (new Point3D(2, -9, -3), new Point3D(0, -2, 2)),
                (new Point3D(0, -8, 4), new Point3D(0, 1, -2)),
                (new Point3D(1, 1, 5), new Point3D(0, 0, 2)),
            }, system);

            // After 10 steps.
            system.Update();
            checkSystem(new[]
            {
                (new Point3D(2, 1, -3), new Point3D(-3, -2, 1)),
                (new Point3D(1, -8, 0), new Point3D(-1, 1, 3)),
                (new Point3D(3, -6, 1), new Point3D(3, 2, -3)),
                (new Point3D(2, 0, 4), new Point3D(1, -1, -1)),
            }, system);
        }

        [Test]
        public void EnergyTest()
        {
            var system = new GravitySystem(new Body[]
            {
                new Body(new Point3D(-1, 0, 2)),
                new Body(new Point3D(2, -10, -7)),
                new Body(new Point3D(4, -8, 8)),
                new Body(new Point3D(3, 5, -1)),
            });

            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }

            Assert.AreEqual(179, system.Energy);
        }


        [Test]
        public void ApplyGravityLargeTest()
        {
            var system = new GravitySystem(new Body[]
            {
                new Body(new Point3D(-8, -10, 0)),
                new Body(new Point3D(5, 5, 10)),
                new Body(new Point3D(2, -7, 3)),
                new Body(new Point3D(9, -8, -3)),
            });

            // After 0 steps.
            checkSystem(new[]
            {
                (new Point3D(-8, -10, 0), new Point3D(0, 0, 0)),
                (new Point3D(5, 5, 10), new Point3D(0, 0, 0)),
                (new Point3D(2, -7, 3), new Point3D(0, 0, 0)),
                (new Point3D(9, -8, -3), new Point3D(0, 0, 0)),
            }, system);

            // After 10 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(-9, -10, 1), new Point3D(-2, -2, -1)),
                (new Point3D(4, 10, 9), new Point3D(-3, 7, -2)),
                (new Point3D(8, -10, -3), new Point3D(5, -1, -2)),
                (new Point3D(5, -10, 3), new Point3D(0, -4, 5)),
            }, system);

            // After 20 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(-10, 3, -4), new Point3D(-5, 2, 0)),
                (new Point3D(5, -25, 6), new Point3D(1, 1, -4)),
                (new Point3D(13, 1, 1), new Point3D(5, -2, 2)),
                (new Point3D(0, 1, 7), new Point3D(-1, -1, 2)),
            }, system);

            // After 30 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(15, -6, -9), new Point3D(-5, 4, 0)),
                (new Point3D(-4, -11, 3), new Point3D(-3, -10, 0)),
                (new Point3D(0, -1, 11), new Point3D(7, 4, 3)),
                (new Point3D(-3, -2, 5), new Point3D(1, 2, -3)),
            }, system);

            // After 40 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(14, -12, -4), new Point3D(11, 3, 0)),
                (new Point3D(-1, 18, 8), new Point3D(-5, 2, 3)),
                (new Point3D(-5, -14, 8), new Point3D(1, -2, 0)),
                (new Point3D(0, -12, -2), new Point3D(-7, -3, -3)),
            }, system);

            // After 50 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(-23, 4, 1), new Point3D(-7, -1, 2)),
                (new Point3D(20, -31, 13), new Point3D(5, 3, 4)),
                (new Point3D(-4, 6, 1), new Point3D(-1, 1, -3)),
                (new Point3D(15, 1, -5), new Point3D(3, -3, -3)),
            }, system);

            // After 60 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(36, -10, 6), new Point3D(5, 0, 3)),
                (new Point3D(-18, 10, 9), new Point3D(-3, -7, 5)),
                (new Point3D(8, -12, -3), new Point3D(-2, 1, -7)),
                (new Point3D(-18, -8, -2), new Point3D(0, 6, -1)),
            }, system);

            // After 70 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(-33, -6, 5), new Point3D(-5, -4, 7)),
                (new Point3D(13, -9, 2), new Point3D(-2, 11, 3)),
                (new Point3D(11, -8, 2), new Point3D(8, -6, -7)),
                (new Point3D(17, 3, 1), new Point3D(-1, -1, -3)),
            }, system);

            // After 80 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(30, -8, 3), new Point3D(3, 3, 0)),
                (new Point3D(-2, -4, 0), new Point3D(4, -13, 2)),
                (new Point3D(-18, -7, 15), new Point3D(-8, 2, -2)),
                (new Point3D(-2, -1, -8), new Point3D(1, 8, 0)),
            }, system);

            // After 90 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(-25, -1, 4), new Point3D(1, -3, 4)),
                (new Point3D(2, -9, 0), new Point3D(-3, 13, -1)),
                (new Point3D(32, -8, 14), new Point3D(5, -4, 6)),
                (new Point3D(-1, -2, -8), new Point3D(-3, -6, -9)),
            }, system);

            // After 100 steps.
            for (int i = 0; i < 10; i++)
            {
                system.Update();
            }
            checkSystem(new[]
            {
                (new Point3D(8, -12, -9), new Point3D(-7, 3, 0)),
                (new Point3D(13, 16, -3), new Point3D(3, -11, -5)),
                (new Point3D(-29, -11, -1), new Point3D(-3, 7, 4)),
                (new Point3D(16, -13, 23), new Point3D(7, 1, 1)),
            }, system);
        }

        [Test]
        public void EnergyLargeTest()
        {
            var system = new GravitySystem(new Body[]
            {
                new Body(new Point3D(-8, -10, 0)),
                new Body(new Point3D(5, 5, 10)),
                new Body(new Point3D(2, -7, 3)),
                new Body(new Point3D(9, -8, -3)),
            });
            for (int i = 0; i < 100; i++)
            {
                system.Update();
            }
            Assert.AreEqual(1940, system.Energy);
        }

        [Test]
        public void PeriodTest()
        {
            var system = new GravitySystem(new Body[]
            {
                new Body(new Point3D(-1, 0, 2)),
                new Body(new Point3D(2, -10, -7)),
                new Body(new Point3D(4, -8, 8)),
                new Body(new Point3D(3, 5, -1)),
            });

            Assert.AreEqual(2772, system.CalculatePeriod());
        }

        [Test]
        public void PeriodLargeTest()
        {
            var system = new GravitySystem(new Body[]
            {
                new Body(new Point3D(-8, -10, 0)),
                new Body(new Point3D(5, 5, 10)),
                new Body(new Point3D(2, -7, 3)),
                new Body(new Point3D(9, -8, -3)),
            });

            Assert.AreEqual(4686774924, system.CalculatePeriod());
        }


        private void checkSystem((Point3D pos, Point3D vel)[] expected, GravitySystem actual)
        {
            for (int i = 0; i < expected.Count(); i++)
            {
                Point3D ePos = expected[i].pos;
                Point3D eVel = expected[i].vel;
                Point3D aPos = actual.Bodies.ElementAt(i).Position;
                Point3D aVel = actual.Bodies.ElementAt(i).Velocity;

                Assert.AreEqual(ePos.X, aPos.X);
                Assert.AreEqual(ePos.Y, aPos.Y);
                Assert.AreEqual(ePos.Z, aPos.Z);

                Assert.AreEqual(eVel.X, aVel.X);
                Assert.AreEqual(eVel.Y, aVel.Y);
                Assert.AreEqual(eVel.Z, aVel.Z);
            }
        }
    }
}