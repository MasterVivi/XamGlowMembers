using System;
namespace members.Core.Models
{
    public class ResponseWrapper<T>
    {
        public ResponseWrapper(T response, bool partial)
        {
            this.Response = response;
            this.HasPartialContent = partial;
        }

        public T Response { get; }
        public bool HasPartialContent { get; }
    }
}