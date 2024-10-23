using System;
using System.Runtime.Serialization;

namespace MT.Backend.Challenge.Domain.Exceptions
{
    [Serializable]
    public class SettingsNotFoundException : Exception
    {
        private const string MESSAGE = "Settings not found";
        public SettingsNotFoundException() : base(MESSAGE) { }
        public SettingsNotFoundException(string message) : base($"{MESSAGE} {message}") { }
        protected SettingsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
