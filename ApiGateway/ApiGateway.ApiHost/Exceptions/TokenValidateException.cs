namespace ApiGateway.ApiHost.Exceptions
{
    [Serializable]
    internal class TokenValidateException : Exception
    {
        public TokenValidateException(string? message) : base(message)
        {
        }
    }
}