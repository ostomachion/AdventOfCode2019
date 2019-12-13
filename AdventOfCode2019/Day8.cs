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
    // https://adventofcode.com/2019/day/8
    public static class Day8
    {
        public enum SpaceImageColor { Black = 0, White = 1, Transparent = 2 }

        public class SpaceImage
        {
            public SpaceImageLayer[] Layers { get; }

            public SpaceImage(int width, int height, string textData)
            {
                int[] data = textData.Select(x => Int32.Parse(x.ToString())).ToArray();

                int layerSize = width * height;
                int layers = data.Length / layerSize;

                Layers = new SpaceImageLayer[layers];
                for (int layer = 0; layer < layers; layer++)
                {
                    SpaceImageColor[,] layerData = new SpaceImageColor[width, height];
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            layerData[x, y] = (SpaceImageColor)data[layer * layerSize + y * width + x];
                        }
                    }
                    Layers[layer] = new SpaceImageLayer(layerData);
                }
            }

            public SpaceImageLayer Flatten()
            {
                SpaceImageLayer value = new SpaceImageLayer(Layers[0].Data);

                foreach(var layer in Layers)
                {
                    for (int y = 0; y < Layers[0].Height; y++)
                    {
                        for (int x = 0; x < Layers[0].Width; x++)
                        {
                            if (value.Data[x,y] == SpaceImageColor.Transparent && layer.Data[x,y] != SpaceImageColor.Transparent)
                            {
                                value.Data[x, y] = layer.Data[x, y];
                            }
                        }
                    }
                }

                return value;
            }

            public void Show()
            {
                var flattened = Flatten();
                for (int y = 0; y < flattened.Height; y++)
                {
                    for (int x = 0; x < flattened.Width; x++)
                    {
                        var color = flattened.Data[x, y];
                        Console.Write(color == SpaceImageColor.Black ? "  " : color == SpaceImageColor.White ? "##" : throw new NotSupportedException());
                    }
                    Console.WriteLine();
                }
            }
        }
        public class SpaceImageLayer
        {
            public int Width => Data.GetLength(0);
            public int Height => Data.GetLength(1);
            public SpaceImageColor[,] Data { get; }

            public SpaceImageLayer(SpaceImageColor[,] data)
            {
                this.Data = data;
            }
        }
    }
}
