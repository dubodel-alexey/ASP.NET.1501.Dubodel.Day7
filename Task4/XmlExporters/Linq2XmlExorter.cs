using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BookEntity;
using BookListService.Interfaces;
using NLog;

namespace XmlExporters
{
    public class Linq2XmlExorter : IXmlFormatExporter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void Export(IEnumerable<Book> books, string fileName)
        {
            if (books == null)
            {
                logger.Error("SaveBooks. parameter books in null.");
                throw new ArgumentNullException("books");
            }
            try
            {
                var rootElement = new XElement("books");
                var xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"), rootElement);
                foreach (var book in books)
                {
                    var bookElement = new XElement("book",
                                         new XElement("author", book.Author),
                                         new XElement("title", book.Title),
                                         new XElement("publisher", book.Publisher),
                                         new XElement("ISBN", book.ISBN));
                    rootElement.Add(bookElement);
                }
                xDocument.Save(fileName);

            }
            catch (Exception e)
            {
                logger.ErrorException("Error occur during exporting books to file ", e);
                throw;
            }

        }
    }
}
