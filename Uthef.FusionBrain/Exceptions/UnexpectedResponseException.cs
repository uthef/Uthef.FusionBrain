namespace Uthef.FusionBrain.Exceptions
{
    public class UnexpectedResponseException : Exception
    {
        internal UnexpectedResponseException() : base ("Unable to parse response data") { }
    }
}
