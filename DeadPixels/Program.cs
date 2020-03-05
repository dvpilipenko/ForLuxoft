using System;
using System.Collections.Generic;

namespace DeadPixels
{
    class Program
    {
        static void Main(string[] args)
        {

            var watch = new System.Diagnostics.Stopwatch();
            DeadPixels app = new DeadPixels();

            // Test 1
            char[][] arr =
            {
                new char[] {'1', '0', '1' },
                new char[] {'1', '0', '1' },
                new char[] {'1', '0', '1' },

            };
         
            watch.Start();
            int countTest1 = app.CountGroups(arr);
            watch.Stop();
            Console.WriteLine("Test 1 Count: " + countTest1 + " $Time: " + watch.ElapsedMilliseconds + " ms");

            //Test 2 Big Data
            var generator = new Random();
            char[][] bigArr = new char[4320][];
            for (int y = 0; y < 4320; y++)
            {
                bigArr[y] = new char[7680];
                for (int x = 0; x < 7680; x++)
                {
                    bigArr[y][x] = generator.Next(0, 2).ToString()[0];
                }
            }

            watch.Reset();
            watch.Start();
            int countTestBigData = app.CountGroups(bigArr);
            watch.Stop();
            Console.WriteLine("Test 2 Big Data Count: " + countTestBigData + " $Time: " + watch.ElapsedMilliseconds + " ms");
            Console.ReadLine();

        }

        //single pixel represent for human-readable code
        public class Pixel
        {
            public Pixel(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }

        }

        public class DeadPixels
        {
            public int CountGroups(char[][] monitor)
            {
                int countGroup = 0;

                //this is just to avoid a side effect if you use the same object multiple times
                char[][] clonedMonitor = monitor.Clone() as char[][];

                for (int y = 0; y < clonedMonitor.Length; y++)
                {
                    for (int x = 0; x < clonedMonitor[y].Length; x++)
                    {
                        if (Equals(clonedMonitor[y][x], '1'))
                        {
                            //if we come across a Dead pixel, delete the group of these pixels
                            ClearGroupDeadPixels(clonedMonitor, x, y);
                            countGroup++;
                        }
                    }
                }
                return countGroup;
            }


            // spatial complexity that method is O((x*y)^2)
            public void ClearGroupDeadPixels(char[][] monitor, int x, int y)
            {
                List<Pixel> pixels = new List<Pixel>();
                pixels.Add(new Pixel(x, y));
                monitor[y][x] = '0';

                //iterate through the list of Pixels adjacent to other dead pixels, and add to the list if necessary
                for (int i = 0; i < pixels.Count; i++)
                {
                    Pixel pixel = pixels[i];

                    // Check Right side
                    if (pixel.X + 1 < monitor[pixel.Y].Length && Equals(monitor[pixel.Y][pixel.X + 1], '1'))
                    {
                        pixels.Add(new Pixel(pixel.X + 1, pixel.Y));
                        monitor[pixel.Y][pixel.X +1 ] = '0';
                    }

                    // Check Left Side
                    if (pixel.X - 1 >= 0 && Equals(monitor[pixel.Y][pixel.X - 1], '1'))
                    {
                        pixels.Add(new Pixel(pixel.X - 1, pixel.Y));
                        monitor[pixel.Y][pixel.X - 1] = '0';
                    }
                    // Check Bottom
                    if (pixel.Y + 1 < monitor.Length && Equals(monitor[pixel.Y + 1][pixel.X], '1'))
                    {
                        pixels.Add(new Pixel(pixel.X, pixel.Y + 1));
                        monitor[pixel.Y + 1][pixel.X] = '0';
                    }

                    // Check Top
                    if (pixel.Y - 1 >= 0 && Equals(monitor[pixel.Y - 1][pixel.X], '1')) {
                        pixels.Add(new Pixel(pixel.X, pixel.Y - 1));
                        monitor[pixel.Y - 1][pixel.X] = '0';
                    }
                }
            }

        }
    }

}
