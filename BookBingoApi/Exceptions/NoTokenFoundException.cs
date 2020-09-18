using System;
using System.Runtime.Serialization;

namespace BookBingoApi.Exceptions
{
    [Serializable]
    public class NoTokenFoundException : Exception
    {
        public NoTokenFoundException(string tokenId) : base($"Token with id ${tokenId} not found") 
        {
        }

        // Without this constructor, deserialization will fail
        protected NoTokenFoundException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }
    }
}
