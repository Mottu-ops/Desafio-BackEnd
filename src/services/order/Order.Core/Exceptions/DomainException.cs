namespace Order.Core.Exceptions
{
    public class DomainException : Exception {
        public List<string>? Errors { get; }

        public DomainException() {}
        public DomainException(string message, List<string> errors): base(message) {
            Errors = errors;
        }
        public DomainException(string message) : base(message) {}

        public DomainException(string message, Exception innerException) : base(message, innerException) {}
    }
}