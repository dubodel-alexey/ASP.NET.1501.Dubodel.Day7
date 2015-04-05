using System;
using System.Collections.Generic;
using System.IO;
using BookEntity;
using BookListService.Interfaces;
using NLog;

namespace BookRepositories
{
    public class BinaryFileRepository : IBookRepository
    {
        public string FileName { get; private set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BinaryFileRepository(string fileName)
        {
            FileName = fileName;
        }

        public IEnumerable<Book> LoadBooks()
        {
            var books = new List<Book>();
            try
            {
                using (var fileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (var repository = new BinaryReader(fileStream))
                    {
                        while (fileStream.Length > fileStream.Position)
                        {
                            books.Add(new Book
                            {
                                Author = repository.ReadString(),
                                Title = repository.ReadString(),
                                ISBN = repository.ReadString(),
                                Publisher = repository.ReadString()
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.ErrorException("Error occur during loading books from file.", e);
                throw;
            }

            return books;
        }

        public void SaveBooks(IEnumerable<Book> books)
        {
            if (books == null)
            {
                logger.Error("SaveBooks. parameter books in null.");
                throw new ArgumentNullException("books");
            }

            try
            {
                using (var repository = new BinaryWriter(new FileStream(FileName, FileMode.Create, FileAccess.Write)))
                {
                    foreach (var book in books)
                    {
                        repository.Write(book.Author);
                        repository.Write(book.Title);
                        repository.Write(book.ISBN);
                        repository.Write(book.Publisher);
                    }
                    repository.Flush();
                }
            }
            catch (Exception e)
            {
                logger.ErrorException("Error occur during saving books to file.", e);
                throw;
            }
        }
    }
}
