class TrapezoidCorrector
{
    public Quadrilateral Quadrilateral;
    public int Rows;
    public int Columns;
    // Create a class constructor with a parameter
    public TrapezoidCorrector(Quadrilateral quadrilateral, int rows, int columns)
    {
        Quadrilateral = quadrilateral;
        Rows = rows;
        Columns = columns;
    }
    public List<Coordinate> getStartingPoints()
    {
        List<Coordinate> startRowCoordinates = GetLineCoordinates(Quadrilateral.TopLeft, Quadrilateral.TopRight, Rows);
        List<Coordinate> endRowCoordinates = GetLineCoordinates(Quadrilateral.BottomLeft, Quadrilateral.BottomRight, Rows);
        List<Coordinate> startPositions = new List<Coordinate>();
        for (int i = 0; i < startRowCoordinates.Count; i++)
        {
            List<Coordinate> startRowPositions = GetLineCoordinates(startRowCoordinates[i], endRowCoordinates[i], Columns);
            startPositions.AddRange(startRowPositions);
        }
        return startPositions;
    }
    static List<Coordinate> GetLineCoordinates(Coordinate start, Coordinate end, int numDelta)
    {
        // Convert two coordinate into an line equation
        double deltaX = (end.X - start.X) / numDelta;
        double deltaY = (end.Y - start.Y) / numDelta;
        List<Coordinate> result = new List<Coordinate>();
        for (int i = 1; i < numDelta; i++)
        {
            result.Add(new Coordinate(start.X + deltaX * i, start.Y + deltaY * i));
        }
        return result;
    }
}

public class Coordinate
{
    public double X;
    public double Y;
    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public class Quadrilateral
{
    public Coordinate TopLeft;
    public Coordinate TopRight;
    public Coordinate BottomLeft;
    public Coordinate BottomRight;
    public Quadrilateral(Coordinate topLeft, Coordinate topRight, Coordinate bottomLeft, Coordinate bottomRight)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
    }
    public double[,] convertToDouble()
    {
        double[,] result =
        {
            { TopLeft.X, TopLeft.Y },
            { TopRight.X, TopRight.Y },
            { BottomLeft.X, BottomLeft.Y },
            { BottomRight.X, BottomRight.Y }
        };
        return result;
    }
};