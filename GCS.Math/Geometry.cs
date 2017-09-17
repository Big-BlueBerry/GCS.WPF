using math = System.Math;
using Vector2 = System.Windows.Point;

namespace GCS.Math
{
    public static class Geometry
    {
        #region GetIntersects
        public static Vector2[] GetIntersects(ILine line1, ILine line2)
        {
            if (Grad(line1) == Grad(line2))
            {
                if (Yint(line1) == Yint(line2))
                    throw new SameShapeException();
                else
                    return new Vector2[] { };
            }
            float intersect_x = (Yint(line2) - Yint(line1)) / (Grad(line1) - Grad(line2));
            return new[] { new Vector2(intersect_x, intersect_x * Grad(line1) + Yint(line1)) };
        }

        public static Vector2[] GetIntersects(ISegment seg1, ISegment seg2)
        {
            if (Grad(seg1) == Grad(seg2))
            {
                if (Yint(seg1) == Yint(seg2))
                    throw new SameShapeException();
                else
                    return new Vector2[] { };
            }
            
            float intersect_x = (Yint(seg2) - Yint(seg1)) / (Grad(seg1) - Grad(seg2));
            Vector2 intersect = new Vector2(intersect_x, intersect_x * Grad(seg1) + Yint(seg1));
            var seg1_len = seg1.Point1.Distance(seg1.Point2);
            var seg2_len = seg2.Point1.Distance(seg2.Point2);
            if (intersect.Distance(seg1.Point1) < seg1_len &&
                intersect.Distance(seg1.Point2) < seg1_len &&
                intersect.Distance(seg2.Point1) < seg2_len &&
                intersect.Distance(seg2.Point2) < seg2_len)
            {
                return new Vector2[] { intersect };
            }
            else return new Vector2[] { };
        }
        #endregion

        public static Vector2 GetNearest(IShape shape, Vector2 point)
        {
            throw new WorkWoorimException();
        }

        public static float GetNearestDistance(IShape shape, Vector2 point)
        {
            throw new WorkWoorimException();
        }

        public static float Grad(ILineLike line)
            => (float)((line.Point2.Y - line.Point1.Y) / (line.Point2.X - line.Point1.X));
        public static float Yint(ILineLike line)
            => (float)(line.Point1.Y - Grad(line) * line.Point1.X);
        public static float Distance(this Vector2 p1, Vector2 p2)
            => (float)math.Sqrt(math.Pow(p2.X - p1.X, 2) + math.Pow(p2.Y - p1.Y, 2));
    }
}
