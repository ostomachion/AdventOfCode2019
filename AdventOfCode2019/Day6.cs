using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/6
    public static class Day6
    {
        public static int CountOrbits(string[] map)
        {
            return OrbitMap.FromList(map).Walk().Sum(x => x.GetDepth());
        }

        public static int MinimumOrbitTransfers(string[] map, string sourceLabel, string destinationLabel)
        {
            var com = OrbitMap.FromList(map);
            var source = com[sourceLabel].Parent;

            int transfers = 0;
            var commonAncestor = source;
            while (commonAncestor[destinationLabel] == null)
            {
                commonAncestor = commonAncestor.Parent;
                transfers++;
            }

            var node = com[destinationLabel].Parent;
            while (node != commonAncestor)
            {
                node = node.Parent;
                transfers++;
            }

            return transfers;
        }

        public class OrbitMap
        {
            private readonly List<OrbitMap> orbits = new List<OrbitMap>();
            private OrbitMap parent;

            public string Label { get; }
            public IEnumerable<OrbitMap> Orbits => this.orbits;
            public OrbitMap Parent => this.parent;

            private OrbitMap(string label)
            {
                this.Label = label;
            }

            public OrbitMap this[string label] => Walk().SingleOrDefault(x => x.Label == label);

            public static OrbitMap FromList(string[] map)
            {
                Dictionary<string, OrbitMap> nodes = new Dictionary<string, OrbitMap>();
                nodes.Add("COM", new OrbitMap("COM"));
                foreach (var item in map)
                {
                    string label = item.Split(')')[1];
                    nodes.Add(label, new OrbitMap(label));
                }

                foreach (var item in map)
                {
                    string parent = item.Split(')')[0];
                    string child = item.Split(')')[1];
                    nodes[parent].AddOrbit(nodes[child]);
                }

                return nodes["COM"];
            }

            public void AddOrbit(OrbitMap map)
            {
                map.parent = this;
                this.orbits.Add(map);
            }

            public int GetDepth() => this.parent == null ? 0 : 1 + this.parent.GetDepth();

            public IEnumerable<OrbitMap> Walk()
            {
                yield return this;
                foreach (var orbit in this.orbits)
                {
                    foreach (var item in orbit.Walk())
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
