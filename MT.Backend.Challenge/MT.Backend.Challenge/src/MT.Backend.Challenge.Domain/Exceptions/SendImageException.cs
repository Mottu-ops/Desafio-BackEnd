using MT.Backend.Challenge.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MT.Backend.Challenge.Domain.Exceptions
{
   [Serializable]
    public class SendImageException : Exception
    {
        private const string MESSAGE = ServiceConstants.SendImageFail;
        public SendImageException() : base(MESSAGE) { }
        public SendImageException(string message) : base($"{MESSAGE} {message}") { }
        protected SendImageException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
