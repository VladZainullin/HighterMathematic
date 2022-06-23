namespace Mathematics;
public class Point
{
    public Point(in double x, in double y, in double z)
    {
        (X, Y, Z) = (x, y, z);
    }

    public Point()
    {
        (X, Y, Z) = (0, 0, 0);
    }

    public double X { get; protected set; }
    public double Y { get; protected set; }
    public double Z { get; protected set; }
}