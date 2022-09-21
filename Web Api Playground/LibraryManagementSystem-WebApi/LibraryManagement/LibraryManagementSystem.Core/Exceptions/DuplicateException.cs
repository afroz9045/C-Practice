namespace LibraryManagement.Core.Exceptions
{
    [Serializable]
    public class DuplicateException : Exception
    {
        public DuplicateException() : base()
        {
        }

        protected DuplicateException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public DuplicateException(string message) : base(message)
        {
        }

        public DuplicateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}