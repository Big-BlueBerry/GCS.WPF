using System.Collections;
using System.Windows;
using math = System.Math;

namespace GCS.Math
{
    public class PointComparer : IComparer
    {
        public float EPSILON = 0.01f;
        public PointComparer(float epsilon = 0.01f) { EPSILON = epsilon; }
        int IComparer.Compare(object p1, object p2)
        {
            Vector x = (Vector)p1, y = (Vector)p2;
            if (math.Abs(x.X - y.X) <= EPSILON && math.Abs(x.Y - y.Y) <= EPSILON) return 0;
            return -1;
        }
    }
}
