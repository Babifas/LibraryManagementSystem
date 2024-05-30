using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;
        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public async Task<IEnumerable<Books>> GetAllBooks()
        {
            List<Books> books = new List<Books>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books", connection);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    books.Add(new Books
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Publisher = reader["Publisher"].ToString(),
                        YearPublished = Convert.ToInt32(reader["YearPublished"]),
                        CopiesAvailable = Convert.ToInt32(reader["CopiesAvailable"]),
                        TotalCopies = Convert.ToInt32(reader["TotalCopies"])
                    });
                }
                return books;
            }
        }
        public async Task<Books> GetBooksById(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@bookId", connection);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Books
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Publisher = reader["Publisher"].ToString(),
                        YearPublished = Convert.ToInt32(reader["YearPublished"]),
                        CopiesAvailable = Convert.ToInt32(reader["CopiesAvailable"]),
                        TotalCopies = Convert.ToInt32(reader["TotalCopies"])
                    };
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<bool> AddNewBook(Books book)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO BOOKS VALUES(@Title,@Author,@Publisher,@YearPublished,@CopiesAvailable,@TotalCopies)",connection);
                cmd.Parameters.AddWithValue("@Title",book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@CopiesAvailable", book.CopiesAvailable);
                cmd.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                var rowsEffected=await cmd.ExecuteNonQueryAsync();
                if (rowsEffected > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
