using System.Collections.Generic;
using BookEntity;

namespace BookListService.Interfaces
{
    public interface IXmlFormatExporter
    {
        void Export(IEnumerable<Book> books, string fileName);
    }
}
