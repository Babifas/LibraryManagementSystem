using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Repository
{
    public class BorrowRepository:IBorrowRepository
    {
        private readonly string _connectionString;
        public BorrowRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public async Task<IEnumerable<Borrow>> GetAllBorrows()
        {
            List<Borrow> borrows = new List<Borrow>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Borrowing", connection);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    borrows.Add(new Borrow
                    {
                        BorrowId = Convert.ToInt32(reader["BorrowId"]),
                        BookId = Convert.ToInt32(reader["BookId"]),
                        MemberId = Convert.ToInt32(reader["MemberId"]),
                        BorrowDate = Convert.ToDateTime(reader["BorrowDate"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"]),
                        ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : (DateTime?)null,
                        Status = reader["Status"].ToString(),
                    });
                }
                return borrows;
            }
        }
        public async Task<bool> BorrowBook(Borrow borrowDetails)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("INSERT INTO Borrowing(BookId,memberId,BorrowDate,DueDate,,Status) VALUES(@BookId,@memberId,@BorrowDate,@DueDate,@Status)", connection);
                cmd.Parameters.AddWithValue("@BookId", borrowDetails.BookId);
                cmd.Parameters.AddWithValue("@MemberId", borrowDetails.MemberId);
                cmd.Parameters.AddWithValue("@BorrowDate", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@DueDate", borrowDetails.DueDate);
                cmd.Parameters.AddWithValue("@Status", "Borrowed");
                var rowAffected=await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        }
        public async Task<bool> ReturnBook(int borrowId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("UPDATE Borrowing SET ReturnDate=@ReturnDate, Status=@Status WHERE BorrowId=@BorrowId", connection); cmd.Parameters.AddWithValue("@BorrowId", borrowId);
                cmd.Parameters.AddWithValue("@ReturnDate", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@Status","Returned");
                var rowAffected = await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        }
    }
}
