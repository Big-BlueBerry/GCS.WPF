using System;
using System.Windows;
using System.Windows.Shapes;

namespace GCS.WPF.GShapes
{
    public class GDot : GShape
    {
        protected override int _attrCount => 1;
        protected override Point this[int index]
        {
            get => index == 0 ? Coord : throw new IndexOutOfRangeException();
            set
            {
                if (index == 0) Coord = value;
                else throw new IndexOutOfRangeException();
            }
        }
        public Point Coord { get; protected set; }

        protected GDot(Ellipse dot = null)
        {
            Control = dot ?? new Ellipse();
        }

        protected GDot(Point coord) : this()
        {
            (Control as Ellipse).SetDot(coord);
            Coord = coord;
        }
    }
}
