using System;
using BookServiceLogic;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bookService = new BookListService();

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

            Console.ReadKey();
        }
    }
}