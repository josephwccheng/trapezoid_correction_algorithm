using Plotly.NET;
class Visualize
{

    public static void PlotTrapezoid(List<double> XTrapezoid, List<double> YTrapezoid, List<double> x_coords, List<double> y_coords)
    {
        var trapzoid = Chart2D.Chart.Scatter<double, double, string>(x: XTrapezoid, y: YTrapezoid, mode: StyleParam.Mode.Lines);
        var scatter = Chart2D.Chart.Scatter<double, double, string>(x: x_coords, y: y_coords, mode: StyleParam.Mode.Markers);
        var chart = Chart.Combine(new[] { scatter, trapzoid });
        chart.Show();
    }
}