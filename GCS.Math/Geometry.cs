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

        public static Vector2[] GetIntersects(ILine line, ISegment seg)
        {
            try
            {
                ThrowIfInvalidShape(line);
                ThrowIfInvalidShape(seg);
            }
            catch { throw; }

            if (Grad(line) == Grad(seg))
            {
                if (Yint(line) == Yint(seg))
                    throw new SameShapeException();
                else
                    return new Vector2[] { };
            }

            float intersect_x = (Yint(seg) - Yint(line)) / (Grad(line) - Grad(seg));
            Vector2 intersect = new Vector2(intersect_x, intersect_x * Grad(line) + Yint(line));
            var line_len = line.Point1.Distance(line.Point2);
            var seg_len = seg.Point1.Distance(seg.Point2);
            if (intersect.Distance(seg.Point1) < seg_len &&
                intersect.Distance(seg.Point2) < seg_len)
            {
                return new Vector2[] { intersect };
            }
            else return new Vector2[] { };
        }

        public static Vector2[] GetIntersects(ILine line, ICircle cir)
        {
            try
            {
                ThrowIfInvalidShape(line);
                ThrowIfInvalidShape(cir);
            }
            catch { throw; }

            Vector2 d = line.Point2 - line.Point1;
            double dr = d.Length;
            double D = (line.Point1.X - cir.Center.X) * (line.Point2.Y - cir.Center.Y)
                    - (line.Point2.X - cir.Center.X) * (line.Point1.Y - cir.Center.Y);
            double discriminant = Radius(cir) * Radius(cir) * dr * dr - D * D;
            if (discriminant < 0)
                return new Vector2[] { };
            else if (discriminant == 0)
                return new[] { new Vector2(D * d.Y / (dr * dr) + cir.Center.X, -D * d.X / (dr * dr) + cir.Center.Y) };
            else
            {
                double x = D * d.Y / (dr * dr) + cir.Center.X;
                double y = -D * d.X / (dr * dr) + cir.Center.Y;
                float sgnDy = d.Y < 0 ? -1 : 1;
                double xd = sgnDy * d.X * math.Sqrt(discriminant) / (dr * dr);
                double yd = math.Abs(d.Y) * math.Sqrt(discriminant) / (dr * dr);

                return new[]
                {
                    new Vector2(x + xd, y + yd),
                    new Vector2(x - xd, y - yd)
                };
            }
        }

        public static Vector2[] GetIntersects(ISegment seg, ILine line)
            => GetIntersects(line, seg);

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

        public static Vector2[] GetIntersects(ISegment seg, ICircle circle)
        {
            var lin = new MinimalizedShapes.Line(seg.Point1, seg.Point2);
            Vector2[] intersects = GetIntersects(lin, circle);
            float len = Distance(seg.Point1, seg.Point2);
            if (intersects.Length == 0) return new Vector2[] { };
            
            var d01 = Distance(intersects[0], seg.Point1);
            var d02 = Distance(intersects[0], seg.Point2);

            if (intersects.Length == 1)
            {
                if (d01 < len && d02 < len)
                    return intersects;
                else return new Vector2[] { };
            }
            else
            {
                var d11 = Distance(intersects[1], seg.Point1);
                var d12 = Distance(intersects[1], seg.Point2);
                if (d01 < len && d02 < len)
                {
                    if (d11 < len && d12 < len)
                        return intersects;
                    else return new Vector2[] { intersects[0] };
                }
                else if (d11 < len && d12 < len)
                    return new Vector2[] { intersects[1] };
                else return new Vector2[] { };
            }
        }

        public static Vector2[] GetIntersects(ICircle cir, ILine line)
            => GetIntersects(line, cir);

        public static Vector2[] GetIntersects(ICircle cir, ISegment seg)
            => GetIntersects(seg, cir);

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
                float x = (d * d + r1 * r1 - r2 * r2) / (d * 2);
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

        public static void ThrowIfInvalidShape(IEllipse ellipse)
        {

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
