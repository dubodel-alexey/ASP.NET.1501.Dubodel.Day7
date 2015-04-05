using System.Collections.Generic;

namespace BookServiceLogic.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> LoadBooks();
        void SaveBooks(IEnumerable<Book> books);
    }
}
