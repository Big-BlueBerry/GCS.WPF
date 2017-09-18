using Vector2 = System.Windows.Vector;

namespace GCS.Math
{
    public interface IShape { }

    public interface IEllipse : IShape
    {
        Vector2 Focus1 { get; }
        Vector2 Focus2 { get; }
        Vector2 PinPoint { get; }
    }

    public interface ICircle : IShape
    {
        Vector2 Center { get; }
        Vector2 Another { get; }
    }

    public interface ILineLike : IShape
    {
        Vector2 Point1 { get; }
        Vector2 Point2 { get; }
    }

    public interface ILine : ILineLike
    {

    }

    public interface ISegment : ILineLike
    {

    }

    public interface IDot : IShape
    {
        Vector2 Coord { get; }
    }
}
