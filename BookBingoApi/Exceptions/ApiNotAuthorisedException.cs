using System;
using System.Runtime.Serialization;

namespace BookBingoApi.Exceptions
{
    [Serializable]
    public class ApiNotAuthorisedException: Exception
    {
        public ApiNotAuthorisedException(Uri requestUri) : base($"API returned unauthorised for {requestUri}")
        {
        }

        protected ApiNotAuthorisedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
