using System;

namespace GCS.Math
{
    public class GeometryException : Exception { }
    /// <summary>
    /// 도형이 일치할 때 일어나는 에러
    /// </summary>
    public class SameShapeException : GeometryException { }
}
