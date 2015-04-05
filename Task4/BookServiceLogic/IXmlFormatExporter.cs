using System.Collections.Generic;

namespace BookServiceLogic.Interface
{
    public interface IXmlFormatExporter
    {
        void Export(IEnumerable<Book> books, string fileName);
    }
}
