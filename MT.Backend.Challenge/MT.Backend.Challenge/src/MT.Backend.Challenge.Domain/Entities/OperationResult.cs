using System;

namespace MT.Backend.Challenge.Domain.Entities
{
    public class OperationResult<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public Exception Exception { get; set; }  // Use this property carefully.
        
    }
}
