using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Linq;

namespace FindRectangles
{
    class Program
    {
        static void Main(string[] args)
        {


            //var dot = new Dot { xCoord = 1, yCoord = 2 };
            //var dottwo = new Dot { xCoord = 1, yCoord = 2 };
            //Console.WriteLine(dot.GetHashCode());
            //Console.WriteLine(dottwo.GetHashCode());
            //var hashdot = new HashSet<Dot>();
            //hashdot.Add(dot);
            //var contains = hashdot.Contains(dottwo);
            var timeStarted = DateTime.Now;
            Console.WriteLine("Hello World!");
            var path = @"C:\Users\cnez0\source\repos\FindRectangles\test.csv";

            var dots = new List<Dot>();
            var hashDots = new HashSet<Dot>();
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.SetDelimiters(new string[] { "," });
                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    var dot = new Dot();
                    dot.xCoord = Int32.Parse(fields[0]);
                    dot.yCoord = Int32.Parse(fields[1]);
                    dots.Add(dot);
                    hashDots.Add(dot);
                }
            }
            var rectNumbers = 0;

            var numberOfDots = dots.Count;
            for (int i = 0; i < numberOfDots; i++)
            {
                for (int j = i + 1; j < numberOfDots; j++)
                {
                    var slope = FindSlope(dots.ElementAt(i), dots.ElementAt(j));
                    for (int k = j + 1; k < numberOfDots; k++)
                    {
                        var dot = AddSlope(dots.ElementAt(k), slope);
                        if (dot.xCoord >= 0 && dot.yCoord >= 0)
                        {
                            var contains = hashDots.Contains(dot);
                            if (contains && !dot.Equals(dots.ElementAt(k)) && !dot.Equals(dots.ElementAt(j)) && !dot.Equals(dots.ElementAt(i)))
                            {
                                if (CheckRect(dots.ElementAt(i), dots.ElementAt(j), dots.ElementAt(k), dot))
                                {
                                    rectNumbers++;
                                    //Console.WriteLine($"Rect Found with coordinates : {dots.ElementAt(i).xCoord},{dots.ElementAt(i).yCoord} dot one; " +
                                    //    $"{dots.ElementAt(j).xCoord},{dots.ElementAt(j).yCoord} dot two; " +
                                    //    $"{dots.ElementAt(k).xCoord},{dots.ElementAt(k).yCoord} dot three; " +
                                    //    $"{dot.xCoord},{dot.yCoord} dot four; ");
                                }
                            }
                        }
                    }
                }
            }
            var timeEnded = DateTime.Now;
            Console.WriteLine(timeEnded - timeStarted);
            Console.WriteLine(rectNumbers + " rects found");
            Console.ReadKey();

        }
        static int[] FindSlope(Dot dotOne, Dot dotTwo)
        {
            return new int[] { dotTwo.xCoord - dotOne.xCoord, dotTwo.yCoord - dotOne.yCoord };
        }

        static Dot AddSlope(Dot dot, int[] slope)
        {
            return new Dot
            {
                xCoord = dot.xCoord + slope[0],
                yCoord = dot.yCoord + slope[1]
            };
        }

        static Dot addNegativeSlope(Dot dot, int[] slope)
        {
            return new Dot
            {
                xCoord = dot.xCoord - slope[0],
                yCoord = dot.yCoord - slope[1]
            };
        }

        static bool CheckRect (Dot ll, Dot lr, Dot ul, Dot ur) 
        {
            var diagOne = (ll.xCoord - ur.xCoord) * (ll.xCoord - ur.xCoord) + (ll.yCoord - ur.yCoord) * (ll.yCoord - ur.yCoord);
            var diagTwo = (lr.xCoord - ul.xCoord) * (lr.xCoord - ul.xCoord) + (lr.yCoord - ul.yCoord) * (lr.yCoord - ul.yCoord);
            return diagOne == diagTwo;
        }
    }
}
