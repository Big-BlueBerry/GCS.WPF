using System;

namespace GCS.WPF
{
    public class WorkWoorimException : NotImplementedException
    {
        public WorkWoorimException(string message = "") : base(message) { }
    }
}
