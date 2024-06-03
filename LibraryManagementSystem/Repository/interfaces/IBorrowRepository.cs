using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Repository.interfaces
{
    public interface IBorrowRepository
    {
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<bool> BorrowBook(Borrow borrowDetails);
        Task<bool> ReturnBook(int borrowId);
    }
}
