using System;
using BookEntity;
using BookListService.Interfaces;
using Ninject;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            IKernel standartKernel = new StandardKernel(new ConfigModule());

            var mainReposFileName = new Ninject.Parameters.ConstructorArgument("fileName", "repository.txt");
            var secReposFilename = new Ninject.Parameters.ConstructorArgument("fileName", "filteredRepository.txt");

            var mainRepository = standartKernel.Get<IBookRepository>(mainReposFileName);
            var xmlExporter = standartKernel.Get<IXmlFormatExporter>();
            var filteredRepository = standartKernel.Get<IBookRepository>(secReposFilename);

            /*  var mainRepository = new BinaryFormatterRepository("repository.txt");
              var filtredRepository = new BinaryFormatterRepository("filtredRepository.txt");
              var xmlExporter = new Linq2XmlExorter();*/

            var bookService = new BookListService.BookListService(mainRepository);

            bookService.AddBook(new Book
            {
                Author = "Чехов",
                Title = "Толстый и тонкий",
                ISBN = "9785389090941",
                Publisher = "Москва"
            });

            bookService.AddBook(new Book
            {
                Author = "Пушкин",
                Title = "Евгений Онегин",
                ISBN = "9780691019055",
                Publisher = "Princeton University Press"
            });

            bookService.AddBook(new Book
            {
                Author = "Пушкин",
                Title = "Дубровский",
                ISBN = "9785955512433",
                Publisher = "Москва"
            });

            bookService.SortBooks();

            var books = bookService.GetAllBooks();
            Console.WriteLine("GetAllBooks:");
            foreach (var book in books)
            {
                Console.WriteLine("\t" + book);
            }

            books = bookService.GetBooksByAuthor("Пушкин");
            Console.WriteLine("GetBooksByAuthor:");
            foreach (var book in books)
            {
                Console.WriteLine("\t" + book);
            }

            books = bookService.GetBooksByPublisher("Москва");
            Console.WriteLine("GetBooksByPublisher:");
            foreach (var book in books)
            {
                Console.WriteLine("\t" + book);
            }

            bookService.Filter(book => book.Author == "Пушкин", filteredRepository);

            bookService.ExportToXml(xmlExporter, "books.xml");
            Console.ReadKey();
        }
    }
}