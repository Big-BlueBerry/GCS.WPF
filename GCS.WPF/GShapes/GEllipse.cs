using System;
using System.Windows;
using System.Windows.Shapes;
using GCS.Math;
using Vector2 = System.Windows.Vector;
namespace GCS.WPF.GShapes
{
    public class GEllipse : GShape, IEllipse
    {
        public Vector2 Focus1 { get; protected set; }
        public Vector2 Focus2 { get; protected set; }
        public Vector2 PinPoint { get; protected set; }
        //public Point Center => Point.Add(Focus1, Focus2);

        protected override int _attrCount => 3;
        protected override Vector2 this[int index]
        {
            get => new Vector2[] { Focus1, Focus2, PinPoint }[index];
            set
            {
                switch (index)
                {
                    case 0: Focus1 = value; return;
                    case 1: Focus2 = value; return;
                    case 2: PinPoint = value; return;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        protected GEllipse(Ellipse ellipse = null)
        {
            Control = ellipse ?? new Ellipse();
        }

        protected GEllipse(Vector2 focus1, Vector2 focus2, Vector2 pinpoint) : this()
        {
            (Control as Ellipse).SetThreePoint(focus1, focus2, pinpoint);
            Focus1 = focus1;
            Focus2 = focus2;
            PinPoint = pinpoint;
        }
    }
}
