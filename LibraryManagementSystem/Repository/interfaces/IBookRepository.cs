using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs;

namespace LibraryManagementSystem.Repository.interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBooksById(int bookId);
        Task<bool> AddNewBook(Book book);
        Task<bool> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
        Task<IEnumerable<Book>> SearchBooks(BookDto book);
    }
}
