using System.Collections;
using System.Windows;

namespace GCS.Math
{
    public class PointComparer : IComparer
    {
        public const float EPSILON = 0.01f;
        int IComparer.Compare(object p1, object p2)
        {
            Point x = (Point)p1, y = (Point)p2;
            if (x.X - y.X <= EPSILON && x.Y - y.Y <= EPSILON) return 0;
            return -1;
        }
    }
}
