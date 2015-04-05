using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BookServiceLogic.Interface;
using NLog;

namespace BookServiceLogic.XMLExporter
{
    public class XmlWriterExporter : IXmlFormatExporter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public  void Export(IEnumerable<Book> books, string fileName)
        {
            if (books == null)
            {
                logger.Error("SaveBooks. parameter books in null.");
                throw new ArgumentNullException("books");
            }
            try
            {
                var settings = new XmlWriterSettings { Indent = true, WriteEndDocumentOnClose = true };

                using (XmlWriter writer = XmlWriter.Create(fileName, settings))
                {
                    writer.WriteStartElement("books");

                    foreach (var book in books)
                    {
                        writer.WriteStartElement("book");
                        writer.WriteElementString("author", book.Author);
                        writer.WriteElementString("title", book.Title);
                        writer.WriteElementString("publisher", book.Publisher);
                        writer.WriteElementString("ISBN", book.ISBN);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
            catch (Exception e)
            {
                logger.ErrorException("Error occur during exporting books to file ", e);
                throw;
            }
            
        }
    }
}
