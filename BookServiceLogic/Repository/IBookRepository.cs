using System.Collections.Generic;

namespace BookServiceLogic.Repository
{
    interface IBookRepository
    {
        IEnumerable<Book> LoadBooks();
        void SaveBooks(IEnumerable<Book> books);
    }
}
