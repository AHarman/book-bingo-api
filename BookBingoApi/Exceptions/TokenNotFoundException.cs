using System;
using System.Runtime.Serialization;

namespace BookBingoApi.Exceptions
{
    [Serializable]
    public class TokenNotFoundException : Exception
    {
        public TokenNotFoundException(string tokenId) : base($"Token with id ${tokenId} not found") 
        {
        }

        // Without this constructor, deserialization will fail
        protected TokenNotFoundException(SerializationInfo info, StreamingContext context): base(info, context)
        {
        }
    }
}
