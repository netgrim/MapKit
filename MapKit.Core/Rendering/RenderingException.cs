using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Core
{
    public class RenderingException : Exception
    {
        public RenderingException()
        {
        }

        public RenderingException(string message)
            : base(message)
        {
        }

        public RenderingException(string message, Exception innerException)
            :base(message, innerException)
        {}

    }
}
