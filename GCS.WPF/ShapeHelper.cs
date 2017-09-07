using System;
using System.Windows;
using System.Windows.Shapes;

namespace GCS.WPF
{
    public static class ShapeHelper
    {
        public static Ellipse CircleFromTwoDots(double center_x, double center_y, double another_x, double another_y)
            => CircleFromTwoDots(new Point(center_x, center_y), new Point(another_x, another_y));

        public static Ellipse CircleFromTwoDots(Point center, Point another)
        {
            var ellipse = new Ellipse();
            ellipse.SetCircle(center, another);
            return ellipse;
        }

        public static void SetCircle(this Ellipse ellipse, Point center, Point another)
        {
            var radius = Point.Subtract(center, another).Length;
            var x = center.X - radius;
            var y = center.Y - radius;
            ellipse.Width = ellipse.Height = radius * 2;
            ellipse.Margin = new Thickness(x, y, 0, 0);
        }

        public static void SetThreePoint(this Ellipse ellipse, Point focus1, Point focus2, Point pinpoint)
        {
            throw new WorkWoorimException("세 점으로부터 타원의 가로/세로/회전을 조정");
        }

        public static void SetTwoPoint(this Line line, Point p1, Point p2)
        {
            line.X1 = p1.X; line.X2 = p2.X;
            line.Y1 = p1.Y; line.Y2 = p2.Y;
        }

        public static void SetDot(this Ellipse ellipse, Point point)
        {
            ellipse.Width = ellipse.Height = 4;
            ellipse.Margin = new Thickness(point.X - 2, point.Y - 2, 0, 0);
        }
    }
}
