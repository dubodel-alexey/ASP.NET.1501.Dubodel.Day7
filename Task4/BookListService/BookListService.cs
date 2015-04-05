using System;
using System.Collections.Generic;
using System.Linq;
using BookEntity;
using BookListService.Exceptions;
using BookListService.Interfaces;
using NLog;

namespace BookListService
{
    public class BookListService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IBookRepository repository;

        public BookListService(IBookRepository repository)
        {
            this.repository = repository;
        }

        public void AddBook(Book book)
        {
            var books = repository.LoadBooks().ToList();
            if (books.Any(book.Equals))
            {
                logger.Error("Book akready exists; Book: {0}", book);
                throw new BookAlreadyExistsException("Book " + book.Author + " " +
                    book.Title + " " + book.ISBN + " " + book.Publisher + " already exists");
            }
            books.Add(book);
            repository.SaveBooks(books);
        }

        public void SortBooks()
        {
            var booksList = repository.LoadBooks().ToList();
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

        public void Filter(Predicate<Book> predicate, IBookRepository nRepository)
        {
            var books = repository.LoadBooks();
            var bookList = new BookListService(nRepository);

            foreach (Book book in books)
            {
                if (predicate(book))
                    bookList.AddBook(book);
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return repository.LoadBooks();
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            var books = repository.LoadBooks();
            books = books.Where(book => author == book.Author).ToList();
            return books;
        }

        public IEnumerable<Book> GetBooksByPublisher(string publisher)
        {
            var books = repository.LoadBooks();
            return books.Where(book => publisher == book.Publisher).ToList();
        }

        public void ExportToXml(IXmlFormatExporter exporter, string fileName)
        {
            var books = repository.LoadBooks();
            exporter.Export(books, fileName);
        }
    }
}
