using System;
using HomographicMatrix;
using MathNet.Numerics.LinearAlgebra;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
namespace MyApplication
{
    class Program
    {

        static void Main()
        {
            Quadrilateral trapezium = new(
                topLeft: new Coordinate(2, 10),
                topRight: new Coordinate(8, 10),
                bottomLeft: new Coordinate(0, 0),
                bottomRight: new Coordinate(10, 0)
            );
            /*  Obtaining the Transformed Starting Points of the Trapzium */
            TrapezoidCorrector trapezoidCorrector = new(quadrilateral: trapezium, rows: 10, columns: 10);
            List<Coordinate> startPositions = trapezoidCorrector.getStartingPoints();
            var trapeziumChart = Visualize.QuadrilateralChart(trapezium);
            var scatterChart = Visualize.ScatterChart(startPositions);

            /* Homographic Matrics */
            Quadrilateral srcQuad = new(
                topLeft: new Coordinate(0, 1),
                topRight: new Coordinate(1, 1),
                bottomLeft: new Coordinate(0, 0),
                bottomRight: new Coordinate(1, 0)
            );
            // Quadrilateral trgQuad = new( // Scaling up by x2
            //     topLeft: new Coordinate(0, 2),
            //     topRight: new Coordinate(2, 2),
            //     bottomLeft: new Coordinate(0, 0),
            //     bottomRight: new Coordinate(2, 0)
            // );
            Quadrilateral trgQuad = new( // Trapezium
                topLeft: new Coordinate(0.2, 1),
                topRight: new Coordinate(0.8, 1),
                bottomLeft: new Coordinate(0, 0),
                bottomRight: new Coordinate(1, 0)
            );


            Matrix<double> homographicMatrix = DLT.CalculateHomographyMatrix(srcQuad, trgQuad);

            List<Coordinate> srcSquarePoints = // Square points: [x, y]
            new() {
                new Coordinate( 0.1, 0.9 ),
                new Coordinate( 0.9, 0.9 ),
                new Coordinate( 0.1, 0.1),
                new Coordinate( 0.9, 0.1 ),
            };

            List<Coordinate> trgSquarePoints = new();
            for (int i = 0; i < srcSquarePoints.Count; i++)
            {
                Vector<double> src = Vector<double>.Build.DenseOfArray(new double[] { srcSquarePoints[i].X, srcSquarePoints[i].Y });
                Vector<double> target = DLT.Transform(homographicMatrix, src);
                trgSquarePoints.Add(new Coordinate(target[0], target[1]));

            }
            var srcQuadChart = Visualize.QuadrilateralChart(srcQuad);
            var trgQuadChart = Visualize.QuadrilateralChart(trgQuad);
            var srcSquareChart = Visualize.ScatterChart(srcSquarePoints);
            var trgSquareChart = Visualize.ScatterChart(trgSquarePoints);

            /* Plotting the graphs*/
            var trapezoidCorrectorPlot = Chart.Combine(new List<GenericChart.GenericChart> { trapeziumChart, scatterChart });
            var homographicPlot = Chart.Combine(new List<GenericChart.GenericChart> { srcQuadChart, trgQuadChart, srcSquareChart, trgSquareChart });
            var combinedChart = Chart.Grid<IEnumerable<GenericChart.GenericChart>>(2, 1).Invoke(new[] { trapezoidCorrectorPlot, homographicPlot });
            // combinedChart.Show();
            trapezoidCorrectorPlot.Show();
            homographicPlot.Show();
            Console.WriteLine("Program Complete");
        }
    }
}