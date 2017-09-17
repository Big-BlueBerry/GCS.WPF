using System;

namespace GCS.Math
{
    public abstract class GeometryException : Exception
    {
        public GeometryException(string msg = "") : base(msg) { }
    }
    /// <summary>
    /// 도형이 일치할 때 일어나는 에러
    /// </summary>
    public class SameShapeException : GeometryException
    {
        public SameShapeException(string msg = "") : base(msg) { }
    }
    /// <summary>
    /// 도형이 올바른 도형이 아닌 경우 일어나는 에러
    /// </summary>
    public class InvalidShapeException : GeometryException
    {
        public InvalidShapeException(string msg = "") : base(msg) { }
    }
}
