namespace Mathematics;
public sealed class Vector : Point
{
    public Vector() : base() { }

    public Vector(in double x, in double y, in double z) : base(x, y, z) { }

    public Vector(in Point a, in Point b) { X = b.X - a.X; Y = b.Y - a.Y; Z = b.Y - a.Y; }

    public static Vector operator +(in Vector firstVector, in Vector secondVector) =>
        new Vector(firstVector.X + secondVector.X,
            firstVector.Y + secondVector.Y,
            firstVector.Z + secondVector.Z);

    public static Vector operator -(in Vector firstVector, in Vector secondVector) =>
        new Vector(firstVector.X + secondVector.X,
            firstVector.Y + secondVector.Y,
            firstVector.Z + secondVector.Z);

    public Vector GuidingVectors =>
        new Vector(X / Lenght,
                   Y / Lenght,
                   Z / Lenght);

    public static double ScalarMultiply(in Vector firstVector, in Vector secondVector) =>
        firstVector.X * secondVector.X +
        firstVector.Y * secondVector.Y +
        firstVector.Z * secondVector.Z;

    public static Vector VectorMultiply(in Vector firstVector, in Vector secondVector) =>
        new Vector(firstVector.Y * secondVector.Z - firstVector.Z * secondVector.Y,
            firstVector.Z * secondVector.X - firstVector.X * secondVector.Z,
            firstVector.X * secondVector.Y - firstVector.Y * secondVector.X);

    public static double MixedMultiply(in Vector firstVector, in Vector secondVector, in Vector thirdVector) =>
        firstVector.X * secondVector.Y * thirdVector.Z +
        firstVector.Y * secondVector.Z * thirdVector.X +
        firstVector.Z * secondVector.X * thirdVector.Y -
        firstVector.Z * secondVector.Y * thirdVector.X -
        firstVector.X * secondVector.Z * thirdVector.Y -
        firstVector.Y * secondVector.X * thirdVector.Z;

    public static double ParrallepipedSquare(in Vector firstVector, in Vector secondVector) =>
        firstVector.Lenght * secondVector.Lenght * Math.Pow(1 - Math.Pow(CosBetween(firstVector, secondVector), 2), 0.5);

    public static double CosBetween(in Vector firstVector, in Vector secondVector) =>
        ScalarMultiply(firstVector, secondVector) / (firstVector.Lenght * secondVector.Lenght);

    public double GetProjection(in Vector vector) =>
        ScalarMultiply(this, vector) / vector.Lenght;

    public double Lenght => Math.Pow(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2), 0.5);
}