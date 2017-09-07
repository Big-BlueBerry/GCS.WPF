using System;
using System.Windows;
using System.Windows.Shapes;

namespace GCS.WPF.GShapes
{
    public class GEllipse : GShape
    {
        public Point Focus1 { get; protected set; }
        public Point Focus2 { get; protected set; }
        public Point PinPoint { get; protected set; }

        public new Ellipse Control { get; protected set; }

        public GEllipse(Ellipse ellipse = null)
        {
            Control = ellipse ?? new Ellipse();
        }

        public GEllipse(Point focus1, Point focus2, Point pinpoint) : this()
        {
            Control.SetThreePoint(focus1, focus2, pinpoint);
        }
    }
}
