namespace WebApp.Exceptions
{
    [System.Serializable]
    public class ParticipantTooOldException : System.Exception
    {
        public ParticipantTooOldException() { }
        public ParticipantTooOldException(string message) : base(message) { }
        public ParticipantTooOldException(string message, System.Exception inner) : base(message, inner) { }
        protected ParticipantTooOldException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}