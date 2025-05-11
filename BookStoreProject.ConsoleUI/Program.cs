using BookStoreManagement.Application.DTOs.AuthorDtos;
using BookStoreManagement.Application.DTOs.BookDtos;
using BookStoreManagement.Application.DTOs.CustomerDtos;
using BookStoreManagement.Application.DTOs.GenreDtos;
using BookStoreManagement.Application.DTOs.OrderDtos;
using BookStoreManagement.Application.Services;
using BookStoreManagement.Application.Validations;
using BookStoreProject.Domain.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Reflection.Emit;

namespace BookStoreProject.ConsoleUI
{
    internal class Program
    {
        static BookManager bookService = new BookManager();
        static GenreManager genreService = new GenreManager();
        static AuthorManager authorService = new AuthorManager();
        static OrderManager orderService = new OrderManager();
        static CustomerManager customerService = new CustomerManager();
        
        static void Main(string[] args)
        {
            #region Console menu
            Console.WriteLine("*** BOOKSTORE SYSTEM ***");

            Console.WriteLine("1. Kitablar");
            Console.WriteLine(
                "1.1 Yeni Kitab Elave Et\n" +
                "1.2 Kitab Siyahisi\n" +
                "1.3 Kitab Axtar\n" +
                "1.4 Kitabi Redakte Et\n" +
                "1.5 Kitabi Sil\n" +
                "1.6 Kitabin Sayini Artir");
            Console.WriteLine();

            Console.WriteLine("2. Muellifler");
            Console.WriteLine(
                "2.1 Yeni Muellif Elave Et\n" +
                "2.2 Muellif Siyahisi\n" +
                "2.3 Muellifin Kitablari\n" +
                "2.4 Muellifi Sil\n" +
                "2.5 Muellifi Redakte Et");
            Console.WriteLine();

            Console.WriteLine("3. Janrlar");
            Console.WriteLine(
                "3.1 Yeni Janr Elave Et\n" +
                "3.2 Janr Siyahisi\n" +
                "3.3 Janri Sil\n" +
                "3.4 Janri Redakte Et");
            Console.WriteLine();

            Console.WriteLine("4. Musteriler ve Sifarisler");
            Console.WriteLine("4.1 Musteri Qeydiyyati\n" +
                "4.2 Yeni Sifaris\n" +
                "4.3 Sifarislere Baxis\n" +
                "4.4 Sifaris Sil\n" +
                "4.5 Musterilere Baxis\n" +
                "4.6 Musteri Redakte Et\n" +
                "4.7 Musteri Sil");
            Console.WriteLine();

            Console.WriteLine("0. Cixis");
            Console.WriteLine();
            #endregion

            #region Dictionary
            Dictionary<string, Action> commands = new Dictionary<string, Action>
            {
                {"1.1", AddBook },
                {"1.2", GetAllBooks },
                {"1.3", SearchBook },
                {"1.4", UpdateBook },
                {"1.5", DeleteBook },
                {"1.6", IncreseBookQuantity },

                {"2.1", AddAuthor },
                {"2.2", GetAllAuthors},
                {"2.3", GetBooksOfAuthor },
                {"2.4", DeleteAuthor },
                {"2.5", UpdateAuthor },

                {"3.1", AddGenre },
                {"3.2", GetAllGenres },
                {"3.3", DeleteGenre },
                {"3.4", UpdateGenre },

                {"4.1", AddCustomer },
                {"4.2", AddOrder },
                {"4.3", GetAllOrders },
                {"4.4", DeleteOrder },
                {"4.5", GetAllCustomers },
                {"4.6", UpdateCustomer },
                {"4.7", DeleteCustomer },
                {"0", ()=>Environment.Exit(0) }
            };

            do
            {
                string command = Console.ReadLine();

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine("Command cannot be empty.");
                    continue;
                }

                if (commands.TryGetValue(command.ToLower(), out Action action))
                {
                    action.Invoke();
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }
            } while (true);
            #endregion

            #region Add Methods
            static void AddBook()
            {
                Console.WriteLine("***** - ADDING NEW BOOK - *****\n");
                if (!EnterInputString("book Title", out string title))
                    return;

                Console.Write("Enter description: ");
                string description = Console.ReadLine();

                GetAllAuthors();
                if (!EnterInputInt("author Id", out int authorId))
                    return;
                try
                {
                    var auhtor = authorService.GetById(authorId);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Author not found");
                    return;
                }

                GetAllGenres();
                if (!EnterInputInt("genre Id", out int genreId))
                    return;
                try
                {
                    var genre = genreService.GetById(genreId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Genre not found");
                    return;
                }

                if (!EnterInputInt("publish Year", out int year))
                    return;
                if (!EnterInputDouble("price", out double price))
                    return;
                if (!EnterInputInt("quantity in stock", out int quantity))
                    return;

                try
                {
                    var bookCreateDto = new BookCreateDto
                    {
                        Title = title,
                        Description = description,
                        AuthorId = authorId,
                        GenreId = genreId,
                        PublishedYear = year,
                        Price = price,
                        QuantityInStock = quantity
                    };
                    var addedBook=bookService.Add(bookCreateDto);

                    Console.WriteLine("Book added successfully!");
                    PrintBook(addedBook);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void AddGenre()
            {
                Console.WriteLine("***** - ADDING NEW GENRE - *****\n");
                if (!EnterInputString("name", out string genreName))
                    return;

                try
                {
                    var genreCreateDto = new GenreCreateDto
                    {
                        Name = genreName
                    };

                    var addedGenre = genreService.Add(genreCreateDto);
                    Console.WriteLine($"Genre added successfully! (Genre id: {addedGenre.Id})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void AddAuthor()
            {
                Console.WriteLine("***** - ADDING NEW AUTHOR - *****\n");
                if (!EnterInputString("First Name: ", out string authorFirstName))
                    return;
                Console.WriteLine("Last Name: ");
                string authorLastName = Console.ReadLine();

                try
                {
                    var authorCreateDto = new AuthorCreateDto
                    {
                        FirstName = authorFirstName,
                        LastName = authorLastName
                    };
                    var addedAuthor = authorService.Add(authorCreateDto);

                    Console.WriteLine($"Author added successfully! (Author id: {addedAuthor.Id})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void AddCustomer()
            {
                Console.WriteLine("***** - ADDING NEW CUSTOMER - *****\n");
                if (!EnterInputString("First Name: ", out string customerFirstName))
                    return;
                if (!EnterInputString("Last Name: ", out string customerLastName))
                    return;
                if (!EnterInputString("Phone: ", out string customerPhone))
                    return;
                if (!EnterInputString("Email: ", out string customerEmail))
                    return;

                try
                {
                    var customerCreateDto = new CustomerCreateDto
                    {
                        FirstName = customerFirstName,
                        LastName = customerLastName,
                        Phone = customerPhone,
                        Email = customerEmail
                    };
                    var addedCustomer = customerService.Add(customerCreateDto);
                    Console.WriteLine($"Customer Added Successfully! (customer id: {addedCustomer.Id})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void AddOrder()
            {
                Console.WriteLine("*****ADDING NEW ORDER*****\n");
                GetAllCustomers();
                GetAllBooks();
                Console.WriteLine();
                if (!EnterInputInt("customer id", out int customerId))
                    return;

                Console.WriteLine("Enter required date (dd-mm-yyyy): ");
                DateTime.TryParse(Console.ReadLine(), out DateTime requiredDate);

                var bookIds = new List<int>();
                var bookQuantities = new List<int>();


                if (!EnterInputInt("count of book types", out int count))
                    return;
                var bookUpdates = new List<BookUpdateDto>(count);

                for (int i = 0; i < count; i++)
                {
                Point:
                    Console.WriteLine($"Book {i + 1}: ");

                    if (!EnterInputInt("book id", out int bookId))
                        return;

                    if (!EnterInputInt("quantity", out int quantity))
                        return;

                    var book = new BookDto();
                    try
                    {
                        book = bookService.Get(predicate: b => b.Id == bookId, include: b => b.Include(g => g.Genre).Include(a => a.Author));

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Book not found");
                        goto Point;
                    }
                 
                    if (book.QuantityInStock < quantity)
                    {
                        Console.WriteLine($"We have {book.QuantityInStock} books in stock.");

                        goto Point;
                    }
                    else
                    {
                        bookUpdates.Add(new BookUpdateDto
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Description = book.Description,
                            Price = book.Price,
                            PublishedYear = book.PublishedYear,
                            QuantityInStock = book.QuantityInStock - quantity,
                            AuthorId = book.Author.Id,
                            GenreId = book.Genre.Id,
                        });

                        bookQuantities.Add(quantity);
                    }
                    bookIds.Add(bookId);
                }

                try
                {
                    var orderCreateDto = new OrderCreateDto
                    {
                        CustomerId = customerId,
                        OrderDate = DateTime.Now,
                        RequiredDate = requiredDate,
                        BookIds = bookIds,
                        BookQuantities = bookQuantities,
                    };

                    orderService.Add(orderCreateDto);
                    Console.WriteLine("Order added successfully!");
                    foreach (var bookUpdate in bookUpdates)
                    {
                        bookService.Update(bookUpdate);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion

            #region Update Methods
            static void UpdateBook()
            {
                Console.WriteLine("***** - UPDATING BOOK - *****\n");
                GetAllBooks();
                if (!EnterInputInt("book id you want to update.", out int bookId))
                    return;
                var book = new BookDto();
                try
                {
                    book = bookService.Get(predicate: b => b.Id == bookId, include: b => b.Include(g => g.Genre).Include(a => a.Author));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Book not found");
                    return;
                }

                Console.Write("Enter new title: ");
                string newTitle = Console.ReadLine();
                if (newTitle == string.Empty)
                {
                    newTitle = book.Title;
                }

                Console.Write("Enter new description: ");
                string newDescription = Console.ReadLine();
                if (newDescription == string.Empty)
                    newDescription = book.Description;

                int newPublishYear;
                Console.Write("Enter year: ");
                string t = Console.ReadLine();
                if (t != string.Empty)
                    int.TryParse(t, out newPublishYear);
                else
                    newPublishYear = book.PublishedYear;

                Console.Write("Do you want to change author?(yes/no): ");
                string option = Console.ReadLine();
                int newAuthorId = book.Author.Id;
                if (option.ToLower() == "yes")
                {
                    GetAllAuthors();
                    if (!EnterInputInt("new author id", out newAuthorId))
                        return;
                    try
                    {
                        authorService.GetById(newAuthorId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Author not found");
                        return;
                    }
                }
                else if (option.ToLower() != "no")
                {
                    Console.WriteLine("Incorrect input!");
                    return;
                }

                Console.Write("Do you want to change genre?(yes/no): ");
                option = Console.ReadLine();
                int newGenreId = book.Genre.Id;
                if (option.ToLower() == "yes")
                {
                    GetAllGenres();
                    if (!EnterInputInt("new genre id", out newGenreId))
                        return;

                    try
                    {
                        genreService.GetById(newGenreId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Genre not found");
                        return;
                    }
                }
                if (option.ToLower() != "no" && option.ToLower()!="yes")
                {
                    Console.WriteLine("Incorrect input");
                    return;
                }

                Console.Write("Enter new price: ");
                t = Console.ReadLine();
                double newPrice;
                if (t != string.Empty)
                    double.TryParse(t, out newPrice);
                else
                    newPrice = book.Price;

                Console.Write("Enter quantity in stock: ");
                t = Console.ReadLine();
                int newQuantity;
                if (t != string.Empty)
                    int.TryParse(t, out newQuantity);
                else
                    newQuantity = book.QuantityInStock;

                try
                {
                    var bookUpdateDto = new BookUpdateDto
                    {
                        Id = bookId,
                        Title = newTitle,
                        Description = newDescription,
                        Price = newPrice,
                        QuantityInStock = newQuantity,
                        PublishedYear = newPublishYear,
                        GenreId = newGenreId,
                        AuthorId = newAuthorId,
                    };
                    bookService.Update(bookUpdateDto);
                    Console.WriteLine("Book updated successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                var updatedBook = bookService.Get(predicate: b => b.Id == bookId, include: b => b.Include(g => g.Genre).Include(a => a.Author));
                PrintBook(updatedBook);
            }
            
            static void UpdateGenre()
            {
                Console.WriteLine("***** - UPDATING GENRE - *****\n");
                GetAllGenres();
                if (!EnterInputInt("genre id:", out int genreId))
                    return;

                var genre = new GenreDto();
                try
                {
                    genre = genreService.GetById(genreId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Genre not found");
                    return;
                }

                if (!EnterInputString("new name", out string newGenreName))
                    return;

                var genreUpdateDto = new GenreUpdateDto
                {
                    Id=genreId,
                    Name = newGenreName,
                };
                try
                {
                    genreService.Update(genreUpdateDto);
                    Console.WriteLine("Genre updated successfully!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                GetAllGenres();
            }

            static void UpdateAuthor()
            {
                Console.WriteLine("***** - UPDATING AUTHOR - *****\n");
                GetAllAuthors();
                if (!EnterInputInt("author id:", out int authorId))
                    return;

                var author = new AuthorDto();
                try
                {
                    author = authorService.GetById(authorId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Author not found");
                    return;
                }

                if (!EnterInputString("first name", out string newFirstName))
                    return;
                Console.WriteLine("Enter last name");
                string newLastName = Console.ReadLine();

                var authorUpdateDto = new AuthorUpdateDto
                {
                    Id=authorId,
                    FirstName = newFirstName,
                    LastName=newLastName,
                };

                try
                {
                    authorService.Update(authorUpdateDto);
                    Console.WriteLine("Author updated successfully!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                GetAllAuthors();
            }

            static void UpdateCustomer()
            {
                Console.WriteLine("***** - UPDATING CUSTOMER - *****\n");
                GetAllCustomers();
                if (!EnterInputInt("customer id:", out int customerId))
                    return;

                var customer = new CustomerDto();
                try
                {
                    customer = customerService.GetById(customerId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Customer not found");
                    return;
                }

                if (!EnterInputString("first name", out string newFirstName))
                    return;
                if (!EnterInputString("last name", out string newLastName))
                    return;

                Console.Write("Enter new phone: ");
                string newPhone = Console.ReadLine();
                if (newPhone == string.Empty)
                {
                    newPhone = customer.Phone;
                }

                Console.Write("Enter new email: ");
                string newEMail = Console.ReadLine();
                if (newEMail == string.Empty)
                {
                    newEMail = customer.Email;
                }
                var customerUpdateDto = new CustomerUpdateDto
                {
                    Id = customerId,
                    FirstName = newFirstName,
                    LastName = newLastName,
                    Phone= newPhone,
                    Email=newEMail
                };

                try
                {
                    customerService.Update(customerUpdateDto);
                    Console.WriteLine("Customer updated successfully!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                GetAllCustomers();
            }
            #endregion

            #region DeleteMethods
            static void DeleteBook()
            {
                Console.WriteLine("***** - DELETING BOOK - *****");
                GetAllBooks();
                if (!EnterInputInt("Book id", out int bookId))
                    return;

                try
                {
                    bookService.GetById(bookId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Book does not exist");
                    return;
                }

                bookService.Delete(bookId);
                Console.WriteLine($"Book with id {bookId} deleted!");

                GetAllBooks();
            }

            static void DeleteGenre()
            {
                Console.WriteLine("***** - DELETING GENRE - *****\n");
                GetAllGenres();
                if (!EnterInputInt("Genre id", out int genreId))
                    return;

                var genre = new GenreDto();
                try
                {
                    genre = genreService.Get(predicate: g => g.Id == genreId, include: g => g.Include(b => b.Books));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Genre does not exist");
                    return;
                }

                if (genre.Books.Count != 0)
                {
                    Console.WriteLine($"Genre has {genre.Books.Count} books ");
                    foreach (var book in genre.Books)
                        Console.WriteLine($"------------\n{book.Id}     {book.Title}\n----------");

                    Console.WriteLine("Delete books - Enter 1\nUpdate genre of books - Enter 2");
                    string option = Console.ReadLine();
                    if (option == "2")
                    {
                        if (!EnterInputInt("new genre id", out int newGenreId))
                            return;
                        var books = bookService.GetAll(predicate: b => b.GenreId == genreId, include: b => b.Include(g => g.Genre).Include(a => a.Author));

                        foreach (var book in books)
                        {
                            var bookUpdateDto = new BookUpdateDto
                            {
                                Id = book.Id,
                                Title = book.Title,
                                Description = book.Description,
                                AuthorId = book.Author.Id,
                                GenreId = genreId,
                                Price = book.Price,
                                QuantityInStock = book.QuantityInStock,
                                PublishedYear = book.PublishedYear,
                            };
                            try
                            {
                                bookService.Update(bookUpdateDto);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                return;
                            }
                        }

                        Console.WriteLine("***** - Books Updated! - *****\n");
                    }
                    if (option != "1" && option != "2")
                    {
                        Console.WriteLine("Incorrect input!");
                        return;
                    }
                }
                else if (genre.Books.Count == 0)
                    Console.WriteLine("*** - (Genre had no books) - ***");

                authorService.Delete(genreId);
                Console.WriteLine($"Genre with id {genreId} deleted!");

                GetAllGenres();
            }

            static void DeleteAuthor()
            {
                Console.WriteLine("***** - DELETING AUTHOR - *****\n");
                GetAllAuthors();
                if (!EnterInputInt("Author id", out int authorId))
                    return;
                var author = new AuthorDto();
                try
                {
                    author = authorService.Get(predicate:a=>a.Id==authorId, include:a=>a.Include(b=>b.Books));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Author does not exist");
                    return;
                }

                if (author.Books.Count != 0)
                {
                    Console.WriteLine($"Author has {author.Books.Count} books ");
                    foreach(var book in author.Books)
                        Console.WriteLine($"------------\n{ book.Id}     {book.Title}\n----------");
                      
                    Console.WriteLine("Delete author's books - Enter 1\nUpdate author of books - Enter 2");
                    string option = Console.ReadLine();
                    if (option == "2")
                    {
                        if (!EnterInputInt("new author id", out int newAuthorId))
                            return;
                        var books = bookService.GetAll(predicate: b => b.AuthorId == authorId, include: b => b.Include(g => g.Genre).Include(a=>a.Author));

                        foreach (var book in books)
                        {
                            var bookUpdateDto = new BookUpdateDto
                            {
                                Id = book.Id,
                                Title = book.Title,
                                Description = book.Description,
                                AuthorId = newAuthorId,
                                GenreId = book.Genre.Id,
                                Price = book.Price,
                                QuantityInStock = book.QuantityInStock,
                                PublishedYear = book.PublishedYear,
                            };
                            try
                            {
                                bookService.Update(bookUpdateDto);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                return;
                            }
                        }

                        Console.WriteLine("***** - Books Updated! - *****\n");
                    }
                    if (option != "1" && option !="2")
                    {
                        Console.WriteLine("Incorrect input!");
                        return;
                    }
                }
               else  if (author.Books.Count == 0)
                    Console.WriteLine("*** - (Author had no books) - ***");

                authorService.Delete(authorId);
                Console.WriteLine($"Author with id {authorId} deleted!");

                GetAllAuthors();
            }

            static void DeleteCustomer()
            {
                Console.WriteLine("***** - DELETING CUSTOMER - *****\n");
                GetAllCustomers();
                if (!EnterInputInt("Book id", out int customerId))
                    return;

                try
                {
                    customerService.GetById(customerId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Customer does not exist");
                    return;
                }

                customerService.Delete(customerId);
                Console.WriteLine($"Customer with id {customerId} deleted!");

                GetAllCustomers();
            }

            static void DeleteOrder()
            {
                Console.WriteLine("***** - DELETING ORDER - *****\n");
                GetAllOrders();
                if (!EnterInputInt("Order id", out int orderId))
                    return;

                try
                {
                    orderService.GetById(orderId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Order does not exist");
                    return;
                }

                orderService.Delete(orderId);
                Console.WriteLine($"Order with id {orderId} deleted!");
            }
            #endregion

            #region GetMethods
            static void GetAllAuthors()
            {
                var authors = authorService.GetAll();

                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"{"ID",-4} {"Author Name",-40}");
                Console.WriteLine(new string('-', 40));

                foreach (var author in authors)
                    Console.WriteLine(new string($"{author.Id,-4} {author.FullName,-30}"));
                Console.WriteLine(new string('-', 40));
            }

            static void GetAllCustomers()
            {
                var customers = customerService.GetAll();

                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"{"ID",-4} {"Customer Name",-40} {"Phone",-15} {"Email", -15}");
                Console.WriteLine(new string('-', 90));

                foreach (var customer in customers)
                    Console.WriteLine($"{customer.Id,-4} {customer.FullName,-40} {customer.Phone,-15} {customer.Email,-15} ");
                Console.WriteLine(new string('-', 90));
            }

            static void GetAllGenres()
            {
                var genres = genreService.GetAll();

                Console.WriteLine(new string('-', 40));
                Console.WriteLine($"{"ID",-4} {"Genre",-40}");
                Console.WriteLine(new string('-', 40));

                foreach (var genre in genres)
                    Console.WriteLine($"{genre.Id,-4} {genre.Name,-30}");
                Console.WriteLine(new string('-', 40));
            }

            static void GetAllBooks()
            {
                var books = bookService.GetAll(include: x => x.Include(a => a.Author).Include(g => g.Genre));

                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"{"ID",-4} {"Book Title",-20} {"Description",-20} {"Year",-10} " +
                    $"{"Author",-20} {"Genre",-10} {"Price",-10} {"Quantity",-10} ");
                Console.WriteLine(new string('-', 90));

                foreach (var book in books)
                {
                    PrintBook(book);
                }
                Console.WriteLine(new string('-', 40));
            }

            static void GetAllOrders()
            {
                var orders = orderService.GetAll(include: x => x.Include(o => o.Customer).Include(c => c.OrderDetails).ThenInclude(b => b.Book));

                Console.WriteLine(new string('-', 90));
                Console.WriteLine($"{"ID",-4} {"Customer Name",-20} {"Order Date",-15} {"Required Date",-15}");
                Console.WriteLine(new string('-', 90));

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Id,-4} {order.CustomerName,-15} {order.OrderDate,-20}{order.RequiredDate,-15}");
                    Console.WriteLine();
                    Console.WriteLine($"{"Book Title",-20} {"Quantity",-10} {"Total price",-10}");

                    foreach (var detail in order.OrderDetails)
                    {
                        Console.WriteLine($"{detail.BookTitle,-20} {detail.Quantity,-10} {detail.TotalPrice,-10}");
                    }
                    Console.WriteLine(new string('-', 40));
                }
            }

            static void SearchBook()
            {
                Console.WriteLine("***** - SEARCHING BOOK - *****\n");
                Console.WriteLine("Enter 1 for searching book by id;\nEnter 2 for searching book by title");
                string option = Console.ReadLine();
                if (option == "1")
                {
                    if (!EnterInputInt("book id", out int bookId)) ;
                    var book = new BookDto();
                    try
                    {
                       book = bookService.GetById(bookId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Book not found!");
                        return;
                    }
                    PrintBook(book);
                    return;
                }
                if (option == "2")
                {
                    if (!EnterInputString("key: ", out string key))
                        return;
                    var books = new List<BookDto>();
                    try
                    {
                        books = bookService.GetAll(predicate: b => b.Title.ToLower().Contains(key.ToLower()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Not found!");
                        return;
                    }
                    
                    foreach (var book in books)
                    {
                        if (book.Title.ToLower().Contains(key.ToLower()))
                            PrintBook(book);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect command");
                }

            }

            static void GetBooksOfAuthor()
            {
                GetAllAuthors();
                if (!EnterInputInt("author id", out int authorId))
                    return;
                var author = new AuthorDto();
                try
                {
                   author =  authorService.Get(predicate: a => a.Id == authorId, include: a => a.Include(x => x.Books));
                
                }
                catch (Exception ex) {
                    Console.WriteLine("Author not found!");
                    return;                   
                }

                if (author.Books.Count == 0)
                {
                    Console.WriteLine("No Books!");
                    return;
                }

                Console.WriteLine($"Books of {author.FullName} :");
                foreach (var book in author.Books)
                {
                    PrintBook(book);
                }
            }
            #endregion

            static void IncreseBookQuantity(){
                if (!EnterInputInt("book id", out int bookId)) ;
                if (!EnterInputInt("count of new books", out int quantity)) ;

                var book = bookService.Get(b => b.Id == bookId, include: b => b.Include(a => a.Author).Include(g => g.Genre));

                var bookUpdateDto = new BookUpdateDto
                {
                    Id = bookId,
                    Title = book.Title,
                    Price = book.Price,
                    QuantityInStock = book.QuantityInStock + quantity,
                    PublishedYear = book.PublishedYear,
                    AuthorId = book.Author.Id,
                    GenreId = book.Genre.Id,
                };
                try
                {
                    var updatedBook = bookService.Update(bookUpdateDto);
                    Console.WriteLine($"Quantity of Book updated! New quantity: {updatedBook.QuantityInStock}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            static void PrintBook(BookDto book)
            {
                Console.WriteLine($"{book.Id,-4} {book.Title,-20} {book.Description,-20}" +
                $"{book.PublishedYear,-10} {book.AuthorName,-20} {book.GenreName,-10}" +
                $"{book.Price,-10} {book.QuantityInStock,-10}");
            }

            static bool EnterInputString(string s, out string input)
            {
                Console.Write($"Enter {s}: ");
                input = Console.ReadLine();
                if (String.IsNullOrEmpty(input))
                {
                    Console.WriteLine("cannot be empty");
                    return false;
                }
                else return true;
            }

            static bool EnterInputInt(string s, out int result)
            {
                Console.Write($"Enter {s}: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out result))
                {
                    Console.WriteLine($"Invalid {s}");
                    return false;
                }
                else return true;
            }

            static bool EnterInputDouble(string s, out double result)
            {
                Console.Write($"Enter {s}: ");
                string input = Console.ReadLine();
                if (!double.TryParse(input, out result))
                {
                    Console.WriteLine($"invalid {s}");
                    return false;
                }
                else return true;
            }

        }
    }
}
