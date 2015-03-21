using System;
using System.Collections.Generic;
using System.Linq;
using BookServiceLogic.Exceptions;
using BookServiceLogic.Repository;
using NLog;

namespace BookServiceLogic
{
    public class BookListService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string FileName = "repository.bin";
        public void AddBook(Book book)
        {
            var repository = new BinaryFileRepository(FileName);
            var books = repository.LoadBooks();
            if (books.Any(book.Equals))
            {
                logger.Error("Book akready exists; Book: {0}", book);
                throw new BookAlreadyExistsException("Book " + book.Author + " " +
                          book.Title + " " + book.ISBN + " " + book.Publisher + " already exists");
            }
            books = books.Concat(new[] { book });
            repository.SaveBooks(books);
        }
        public void SortBooks()
        {
            var repository = new BinaryFileRepository(FileName);
            var books = repository.LoadBooks();
            var booksList = books as IList<Book> ?? books.ToList();
            for (int i = 0; i < booksList.Count; i++)
            {
                for (int j = i + 1; j < booksList.Count; j++)
                {
                    if (booksList[i].CompareTo(booksList[j]) > 0)
                    {
                        var temp = booksList[i];
                        booksList[i] = booksList[j];
                        booksList[j] = temp;
                    }
                }
            }
            repository.SaveBooks(booksList);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return new BinaryFileRepository(FileName).LoadBooks();
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            var books = new BinaryFileRepository(FileName).LoadBooks();
            books = books.Where(book => author == book.Author).ToList();
            return books;
        }

        public IEnumerable<Book> GetBooksByPublisher(string publisher)
        {
            var books = new BinaryFileRepository(FileName).LoadBooks();
            return books.Where(book => publisher == book.Publisher).ToList();
        }
    }
}
