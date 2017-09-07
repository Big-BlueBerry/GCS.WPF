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
    }
}
