using math = System.Math;
using Vector2 = System.Windows.Vector;

namespace GCS.Math
{
    public static class Geometry
    {
        #region GetIntersects
        public static Vector2[] GetIntersects(ILine line1, ILine line2)
        {
            try
            {
                ThrowIfInvalidShape(line1);
                ThrowIfInvalidShape(line2);
            }
            catch { throw; }

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

        public static Vector2[] GetIntersects(ILine line, ICircle cir)
        {
            throw new WorkWoorimException();
        }

        public static Vector2[] GetIntersects(ISegment seg1, ISegment seg2)
        {
            try
            {
                ThrowIfInvalidShape(seg1);
                ThrowIfInvalidShape(seg2);
            }
            catch { throw; }

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

        public static Vector2[] GetIntersects(ICircle cir1, ICircle cir2)
        {
            try
            {
                ThrowIfInvalidShape(cir1);
                ThrowIfInvalidShape(cir2);
            }
            catch { throw; }

            float distance = cir1.Center.Distance(cir2.Center);
            if (distance == 0)
            {
                if (Radius(cir1) == Radius(cir2)) throw new SameShapeException();
                else return new Vector2[] { };
            }
            else if (Radius(cir1) + Radius(cir2) < distance)
                return new Vector2[] { };
            else if (distance < math.Abs(Radius(cir1) - Radius(cir2)))
                return new Vector2[] { };
            else
            {
                float d = distance;
                float r1 = Radius(cir1);
                float r2 = Radius(cir2);
                float x = (d * d - r1 * r1 - r2 * r2) / (d * 2);
                Vector2 p1 = cir1.Center + (cir2.Center - cir1.Center) * x / d;
                Vector2 v = cir1.Center - cir2.Center;
                v = new Vector2(-v.Y, v.X);
                Vector2 p2 = p1 + v;
                return GetIntersects(new MinimalizedShapes.Line(p1, p2), cir1);
            }
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
        
        public static void ThrowIfInvalidShape(ILineLike shape)
        {
            if (shape.Point1 == shape.Point2) throw new InvalidShapeException();
        }

        public static void ThrowIfInvalidShape(ICircle shape)
        {
            if (shape.Center == shape.Another) throw new InvalidShapeException();
        }

        public static float Grad(ILineLike line)
            => (float)((line.Point2.Y - line.Point1.Y) / (line.Point2.X - line.Point1.X));
        public static float Yint(ILineLike line)
            => (float)(line.Point1.Y - Grad(line) * line.Point1.X);
        public static float Radius(ICircle circle)
            => Distance(circle.Center, circle.Another);
        public static float Distance(this Vector2 p1, Vector2 p2)
            => (float)math.Sqrt(math.Pow(p2.X - p1.X, 2) + math.Pow(p2.Y - p1.Y, 2));
    }
}
