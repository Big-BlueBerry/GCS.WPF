using System;
using System.Windows;
using System.Windows.Shapes;

namespace GCS.WPF.GShapes
{
    public class GDot : GShape
    {
        public Point Coord { get; protected set; }

        public GDot(Ellipse dot = null)
        {
            Control = dot ?? new Ellipse();
        }

        public GDot(Point coord) : this()
        {
            (Control as Ellipse).SetDot(coord);
            Coord = coord;
        }
    }
}
