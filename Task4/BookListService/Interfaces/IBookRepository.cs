using System.Collections.Generic;
using BookEntity;

namespace BookListService.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> LoadBooks();
        void SaveBooks(IEnumerable<Book> books);
    }
}
