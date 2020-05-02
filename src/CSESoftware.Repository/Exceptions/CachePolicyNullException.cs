using System;

namespace CSESoftware.Repository.Exceptions
{
    public class CachePolicyNullException : Exception
    {
        public CachePolicyNullException() : base("The Cache Repository must have a Policy")
        {
        }
    }
}