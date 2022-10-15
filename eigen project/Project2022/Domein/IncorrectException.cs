using System;
using System.Runtime.Serialization;

namespace Project2022.Domein
{
    [Serializable]
    public class IncorrectException : Exception
    {
        public IncorrectException()
        {
        }

        public IncorrectException(string message) : base(message)
        {
        }
    }
}
