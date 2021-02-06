using System;

namespace EDU_OOP_RPG.Exceptions
{
    public class RpgException : Exception
    {
        public RpgException()
        {
        }

        public RpgException(string? message) : base(message)
        {
        }
    }
}