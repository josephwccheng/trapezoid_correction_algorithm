using System;

namespace MyApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            Coordinate topLeft = new Coordinate(2, 10);
            Coordinate topRight = new Coordinate(8, 10);
            Coordinate bottomLeft = new Coordinate(0, 0);
            Coordinate bottomRight = new Coordinate(10, 0);
            TrapezoidCorrector trapezoidCorrector = new TrapezoidCorrector(topLeft, topRight, bottomLeft, bottomRight, 10, 10);
            List<Coordinate> startPositions = trapezoidCorrector.Process();
            List<double> XStart = new List<double>();
            List<double> YStart = new List<double>();
            List<double> XTrapezoid = new List<double> { topLeft.X, topRight.X, bottomRight.X, bottomLeft.X, topLeft.X };
            List<double> YTrapezoid = new List<double> { topLeft.Y, topRight.Y, bottomRight.Y, bottomLeft.Y, topLeft.Y };
            foreach (Coordinate startPosition in startPositions)
            {
                // Accessing elements within each object
                XStart.Add(startPosition.X);
                YStart.Add(startPosition.Y);
            }


            Visualize.PlotTrapezoid(XTrapezoid, YTrapezoid, XStart, YStart);
            Console.WriteLine("Completed");

        }
    }
}