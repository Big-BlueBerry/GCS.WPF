using Vector2 = System.Windows.Vector;

namespace GCS.Math
{
    public sealed class MinimalizedShapes
    {
        public class Ellipse : IEllipse
        {
            public Vector2 Focus1 { get; set; }
            public Vector2 Focus2 { get; set; }
            public Vector2 PinPoint { get; set; }
            public Ellipse(Vector2 focus1, Vector2 focus2, Vector2 pin)
            {
                Focus1 = focus1;
                Focus2 = focus2;
                PinPoint = pin;
            }
        }

        public class Circle : ICircle
        {
            public Vector2 Center { get; set; }
            public Vector2 Another { get; set; }
            public Circle(Vector2 center, Vector2 another)
            {
                Center = center;
                Another = another;
            }
        }

        public abstract class LineLike : ILineLike
        {
            public Vector2 Point1 { get; set; }
            public Vector2 Point2 { get; set; }
            public float Grad => Geometry.Grad(this);
            public float Yint => Geometry.Yint(this);
            public LineLike(Vector2 p1, Vector2 p2)
            {
                Point1 = p1;
                Point2 = p2;
            }
        }

        public class Line : LineLike, ILine
        {
            public Line(Vector2 p1, Vector2 p2) : base(p1, p2) { }
        }

        public class Segment : LineLike, ISegment
        {
            public Segment(Vector2 p1, Vector2 p2) : base(p1, p2) { }
        }

        public class Dot : IDot
        {
            public Vector2 Coord { get; set; }
            public Dot(Vector2 coord)
            {
                Coord = coord;
            }
        }
    }
}
