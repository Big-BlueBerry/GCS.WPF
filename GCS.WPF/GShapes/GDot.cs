using System;
using System.Windows;
using System.Windows.Shapes;
using GCS.Math;

using Vector2 = System.Windows.Vector;

namespace GCS.WPF.GShapes
{
    public class GDot : GShape, IDot
    {
        public Vector2 Coord { get; protected set; }

        protected override int _attrCount => 1;
        protected override Vector2 this[int index]
        {
            get => index == 0 ? Coord : throw new IndexOutOfRangeException();
            set
            {
                if (index == 0) Coord = value;
                else throw new IndexOutOfRangeException();
            }
        }

        protected GDot(Ellipse dot = null)
        {
            Control = dot ?? new Ellipse();
        }

        protected GDot(Vector2 coord) : this()
        {
            (Control as Ellipse).SetDot(coord);
            Coord = coord;
        }

        public static GDot FromCoord(Vector2 coord)
        {
            return new GDot(coord);
        }
    }
}
