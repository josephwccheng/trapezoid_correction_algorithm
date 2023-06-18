class TrapezoidCorrector
{
    public Coordinate TopLeft;
    public Coordinate TopRight;
    public Coordinate BottomLeft;
    public Coordinate BottomRight;
    public int Rows;
    public int Columns;
    // Create a class constructor with a parameter
    public TrapezoidCorrector(Coordinate topLeft, Coordinate topRight, Coordinate bottomLeft, Coordinate bottomRight, int rows, int columns)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
        Rows = rows;
        Columns = columns;
    }

    public static List<Coordinate> GetLineCoordinates(Coordinate start, Coordinate end, int numDelta)
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
    public List<Coordinate> Process()
    {
        List<Coordinate> startRowCoordinates = GetLineCoordinates(TopLeft, TopRight, Rows);
        List<Coordinate> endRowCoordinates = GetLineCoordinates(BottomLeft, BottomRight, Rows);
        List<Coordinate> startPositions = new List<Coordinate>();
        for (int i = 0; i < startRowCoordinates.Count; i++)
        {
            List<Coordinate> startRowPositions = GetLineCoordinates(startRowCoordinates[i], endRowCoordinates[i], Columns);
            startPositions.AddRange(startRowPositions);
        }
        Console.WriteLine($"Stuff has been processed");
        return startPositions;
    }
}

class Coordinate
{
    public double X;
    public double Y;
    public Coordinate(double x, double y)
    {
        X = x;
        Y = y;
    }
}