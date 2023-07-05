using Plotly.NET;
class Visualize
{

    public static GenericChart.GenericChart QuadrilateralChart(Quadrilateral quadrilateral)
    {
        List<double> XQuad = new() { quadrilateral.TopLeft.X, quadrilateral.TopRight.X, quadrilateral.BottomRight.X, quadrilateral.BottomLeft.X, quadrilateral.TopLeft.X };
        List<double> YQuad = new() { quadrilateral.TopLeft.Y, quadrilateral.TopRight.Y, quadrilateral.BottomRight.Y, quadrilateral.BottomLeft.Y, quadrilateral.TopLeft.Y };
        var lineChart = Chart2D.Chart.Scatter<double, double, string>(x: XQuad, y: YQuad, mode: StyleParam.Mode.Lines);
        return lineChart;
    }
    public static GenericChart.GenericChart ScatterChart(List<Coordinate> scatterPoints)
    {
        List<double> x_coords = scatterPoints.Select(obj => obj.X).ToList();
        List<double> y_coords = scatterPoints.Select(obj => obj.Y).ToList();
        var scatterChart = Chart2D.Chart.Scatter<double, double, string>(x: x_coords, y: y_coords, mode: StyleParam.Mode.Markers);
        return scatterChart;
    }
    public static void PlotCharts(List<GenericChart.GenericChart> charts)
    {
        var chart = Chart.Combine(charts);
        chart.Show();
    }
}