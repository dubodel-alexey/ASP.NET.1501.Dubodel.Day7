using System;

namespace BookListService.Exceptions
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
