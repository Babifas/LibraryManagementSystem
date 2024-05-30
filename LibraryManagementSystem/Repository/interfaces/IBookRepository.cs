using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Repository.interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Books>> GetAllBooks();
        Task<Books> GetBooksById(int bookId);
        Task<bool> AddNewBook(Books book);
    }
}
