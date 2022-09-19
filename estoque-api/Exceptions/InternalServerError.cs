using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace storage.Exceptions
{
    [Serializable]
    public class InternalServerError : Exception
    {
        public InternalServerError() : base() { }
        public InternalServerError(string message) : base(message) { }
        public InternalServerError(string message, Exception inner) : base(message, inner) { }
    }
}