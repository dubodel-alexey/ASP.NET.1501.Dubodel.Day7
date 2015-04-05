using System;

namespace BookServiceLogic.Exceptions
{
    class BookAlreadyExistsException : Exception
    {
        public BookAlreadyExistsException()
        {
        }

        public BookAlreadyExistsException(string message)
            : base(message)
        {
        }

    }
}
