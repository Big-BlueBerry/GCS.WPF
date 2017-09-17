using math = System.Math;
using Vector2 = System.Windows.Point;

namespace GCS.Math
{
    public static class Geometry
    {
        #region GetIntersects
        public static Vector2[] GetIntersects(ILine line1, ILine line2)
        {
            if (grad(line1) == grad(line2))
            {
                if (yint(line1) == yint(line2))
                    throw new SameShapeException();
                else
                    return new Vector2[] { };
            }
            float intersect_x = (yint(line2) - yint(line1)) / (grad(line1) - grad(line2));
            return new[] { new Vector2(intersect_x, intersect_x * grad(line1) + yint(line1)) };
        }

        public static Vector2[] GetIntersects(ISegment seg1, ISegment seg2)
        {
            if (grad(seg1) == grad(seg2))
            {
                if (yint(seg1) == yint(seg2))
                    throw new SameShapeException();
                else
                    return new Vector2[] { };
            }
            
            float intersect_x = (yint(seg2) - yint(seg1)) / (grad(seg1) - grad(seg2));
            Vector2 intersect = new Vector2(intersect_x, intersect_x * grad(seg1) + yint(seg1));
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

        private static float grad(ILineLike line)
            => (float)((line.Point2.Y - line.Point1.Y) / (line.Point2.X - line.Point1.X));
        private static float yint(ILineLike line)
            => (float)(line.Point1.Y - grad(line) * line.Point1.X);
        public static float Distance(this Vector2 p1, Vector2 p2)
            => (float)math.Sqrt(math.Pow(p2.X - p1.X, 2) + math.Pow(p2.Y - p1.Y, 2));
    }
}
