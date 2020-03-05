namespace WebApp.Exceptions
{
    
    [System.Serializable]
    public class CapacityReachedException : System.Exception
    {
        public CapacityReachedException() { }
        public CapacityReachedException(string message) : base(message) { }
        public CapacityReachedException(string message, System.Exception inner) : base(message, inner) { }
        protected CapacityReachedException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}