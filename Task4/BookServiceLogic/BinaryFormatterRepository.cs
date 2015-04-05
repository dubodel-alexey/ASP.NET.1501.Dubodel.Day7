using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NLog;

namespace BookServiceLogic.Repository
{
    public class BinaryFormatterRepository : IBookRepository
    {
        public static string FileName { get; private set; }

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Book> LoadBooks()
        {
            List<Book> books;
            try
            {
                using (var fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                {
                    var formatter = new BinaryFormatter();
                    books = (List<Book>)formatter.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                logger.ErrorException("Error occur during reading books from file.", e);
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
                using (var fileStream = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, books);
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
