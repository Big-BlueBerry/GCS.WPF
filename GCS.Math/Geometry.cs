using System;
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

        public static Vector2 GetNearest(ILine line, Vector2 point)
        {
            var grad = (line.Point2.Y - line.Point1.Y) / (line.Point2.X - line.Point1.X);
            var yint = line.Point1.Y - grad * line.Point1.X;
            Vector2 vYint = new Vector2(0, yint);
            Vector2 tempPoint = point - vYint;
            var tempLine = new MinimalizedShapes.Line(line.Point1 - vYint, line.Point2 - vYint);
            var orthogonal = new MinimalizedShapes.Line(tempPoint, new Vector2(tempPoint.X + tempLine.Grad * 10, tempPoint.Y - 10));
            return GetIntersects(tempLine, orthogonal)[0] + vYint;
        }

        public static Vector2 GetNearest(ISegment seg, Vector2 point)
        {
            return GetNearest(new MinimalizedShapes.Line(seg.Point1, seg.Point2), point)
                .Clamp(Min(seg.Point1, seg.Point2), Max(seg.Point1, seg.Point2));
        }

        public static Vector2 GetNearest(ICircle cir, Vector2 point)
        {
            if (cir.Center == point)
            {
                // https://github.com/Big-BlueBerry/GCS.WPF/issues/8
                return cir.Another;
            }
            var line = new MinimalizedShapes.Line(cir.Center, point);
            var res = GetIntersects(line, cir);
            return Distance(res[0], point) < Distance(res[1], point) ? res[0] : res[1];
        }

        public static float GetNearestDistance(IShape shape, Vector2 point)
        {
            // 이렇게 하면 되긴 하지만 꽤 느리게 작동할거야..
            // 시간 나면 디스패치 해서 속도를 향상시키는걸루
            return Distance(GetNearest((dynamic)shape, point), point);
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
            // 두 초점이 일치하면 원
            if (ellipse.Focus1 == ellipse.Focus2 || ellipse.Focus2 == ellipse.PinPoint || ellipse.PinPoint == ellipse.Focus1)
                throw new InvalidShapeException();
            // 두 초점을 잇는 선분 위에 핀포인트가 있다면 직선
            if (Distance(ellipse.Focus1, ellipse.PinPoint) + Distance(ellipse.Focus2, ellipse.PinPoint) == Distance(ellipse.Focus1, ellipse.Focus2))
                throw new InvalidShapeException();
        }

        public static float Grad(ILineLike line)
            => (float)((line.Point2.Y - line.Point1.Y) / (line.Point2.X - line.Point1.X));
        public static float Yint(ILineLike line)
            => (float)(line.Point1.Y - Grad(line) * line.Point1.X);
        public static float Radius(ICircle circle)
            => Distance(circle.Center, circle.Another);
        public static float Distance(this Vector2 p1, Vector2 p2)
            => (float)math.Sqrt(math.Pow(p2.X - p1.X, 2) + math.Pow(p2.Y - p1.Y, 2));
        public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max)
        {
            return new Vector2(v.X.Clamp(min.X, max.X), v.Y.Clamp(min.Y, max.Y));
        }
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
        public static Vector2 Min(Vector2 v1, Vector2 v2)
            => new Vector2(v1.X < v2.X ? v1.X : v2.X,
                           v1.Y < v2.Y ? v1.Y : v2.Y);
        public static Vector2 Max(Vector2 v1, Vector2 v2)
            => new Vector2(v1.X > v2.X ? v1.X : v2.X,
                           v1.Y > v2.Y ? v1.Y : v2.Y);
    }
}
