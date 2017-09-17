using Point = System.Windows.Point;

namespace GCS.Math
{
    public interface IShape
    {
    }

    public interface IEllipse : IShape
    {
        Point Focus1 { get; }
        Point Focus2 { get; }
        Point PinPoint { get; }
    }

    public interface ICircle : IShape
    {
        Point Center { get; }
        Point Another { get; }
    }

    public interface ILine : IShape
    {
        Point Point1 { get; }
        Point Point2 { get; }
    }

    public interface IDot : IShape
    {
        Point Coord { get; }
    }
}
