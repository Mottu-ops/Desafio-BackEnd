namespace User.Domain.Exceptions
{
    public class PersonalizeExceptions : Exception
    {
        internal List<string>? _exception;

        public List<string>? Err => _exception;

        public PersonalizeExceptions()
        {

        }

        public PersonalizeExceptions(string message, List<string> err) : base(message) { }
        public PersonalizeExceptions(string message) : base(message) { }
        public PersonalizeExceptions(string message, Exception inner) : base(message, inner) { }


    }
}
