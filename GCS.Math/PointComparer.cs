using System.Collections;
using System.Windows;

namespace GCS.Math
{
    public class PointComparer : IComparer
    {
        public const float EPSILON = 0.01f;
        int IComparer.Compare(object p1, object p2)
        {
            Vector x = (Vector)p1, y = (Vector)p2;
            if (x.X - y.X <= EPSILON && x.Y - y.Y <= EPSILON) return 0;
            return -1;
        }
    }
}
