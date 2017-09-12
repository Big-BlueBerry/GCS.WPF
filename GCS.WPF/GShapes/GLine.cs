using System.Windows;
using System.Windows.Shapes;

namespace GCS.WPF.GShapes
{
    public class GLine : GShape
    {
        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }

        public GLine(Line line = null)
        {
            Control = line ?? new Line();
        }

        public GLine(Point p1, Point p2) : this()
        {
            (Control as Line).SetTwoPoint(p1, p2);
            Point1 = p1; Point2 = p2;
        }
    }
}
