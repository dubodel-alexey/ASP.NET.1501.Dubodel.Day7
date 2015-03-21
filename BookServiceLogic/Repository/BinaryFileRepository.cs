using System.Collections.Generic;
using System.IO;
using NLog;

namespace BookServiceLogic.Repository
{
    class BinaryFileRepository : IBookRepository
    {
        public static string FileName { get; private set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BinaryFileRepository(string fileName)
        {
            FileName = fileName;
        }
        
        public IEnumerable<Book> LoadBooks()
        {
            var books = new List<Book>();
            if (File.Exists(FileName))
            {
                using (var fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (var repository = new BinaryReader(fs))
                    {
                        while (fs.Length > fs.Position)
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
            else
            {
                logger.Debug("LoadBooks. Can't found file, FileName={0}", FileName);
                throw new FileNotFoundException("LoadBooks. Can't found file: ", FileName);
            }
            return books;
        }

        public void SaveBooks(IEnumerable<Book> books)
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
    }
}
