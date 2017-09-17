using GCS.Math;
using System.Windows;

namespace GCS.MathTests
{
    internal sealed class MinimalizedShapes
    {
        public class Ellipse : IEllipse
        {
            public Point Focus1 { get; set; }
            public Point Focus2 { get; set; }
            public Point PinPoint { get; set; }
            public Ellipse(Point focus1, Point focus2, Point pin)
            {
                Focus1 = focus1;
                Focus2 = focus2;
                PinPoint = pin;
            }
        }

        public class Circle : ICircle
        {
            public Point Center { get; set; }
            public Point Another { get; set; }
            public Circle(Point center, Point another)
            {
                Center = center;
                Another = another;
            }
        }

        public abstract class LineLike : ILineLike
        {
            public Point Point1 { get; set; }
            public Point Point2 { get; set; }
            public float Grad => Geometry.Grad(this);
            public float Yint => Geometry.Yint(this);
            public LineLike(Point p1, Point p2)
            {
                Point1 = p1;
                Point2 = p2;
            }
        }

        public class Line : LineLike, ILine
        {
            public Line(Point p1, Point p2) : base(p1, p2) { }
        }

        public class Segment : LineLike, ISegment
        {
            public Segment(Point p1, Point p2) : base(p1, p2) { }
        }

        public class Dot : IDot
        {
            public Point Coord { get; set; }
            public Dot(Point coord)
            {
                Coord = coord;
            }
        }
    }
}
