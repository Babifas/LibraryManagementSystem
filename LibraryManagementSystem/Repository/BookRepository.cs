using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.Data.SqlClient;
using System.Text;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;
        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books", connection);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    books.Add(new Book
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
        public async Task<Book> GetBooksById(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE BookId=@bookId", connection);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Book
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
        public async Task<bool> AddNewBook(Book book)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title,Author,Publisher,YearPublished,CopiesAvailable,TotalCopies) VALUES(@Title,@Author,@Publisher,@YearPublished,@CopiesAvailable,@TotalCopies)", connection);
                cmd.Parameters.AddWithValue("@Title",book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@CopiesAvailable", book.CopiesAvailable);
                cmd.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                var rowsEffected=await cmd.ExecuteNonQueryAsync();
                return rowsEffected > 0;
            }
        }
        public  async Task<bool> UpdateBook(Book book)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("UPDATE Books SET Title=@Title,Author=@Author,Publisher=@Publisher,YearPublished=@YearPublished,CopiesAvailable=@CopiesAvailable,TotalCopies=@TotalCopies WHERE BookId=@BookId", connection);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@CopiesAvailable", book.CopiesAvailable);
                cmd.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                cmd.Parameters.AddWithValue("@BookId", book.BookId);
                var rowAffected=await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        }
        public async Task<bool> DeleteBook(int bookId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE BookId=@BookId", connection);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                var rowAffected= await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        } 
        public async Task<IEnumerable<Book>> SearchBooks(BookDto book)
        {
            List<Book> books = new List<Book>();
            using (SqlConnection connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                StringBuilder query = new StringBuilder("SELECT * FROM Books WHERE ");
                List<SqlParameter> parameters = new List<SqlParameter>();
                if (book.Title != null)
                {
                    query.Append("Title=@Title AND");
                    parameters.Add(new SqlParameter("@Title", book.Title));
                }
                if (book.Author != null)
                {
                    query.Append("Author=@Author AND");
                    parameters.Add(new SqlParameter("@Author", book.Author));
                }
                if (book.Publisher != null)
                {
                    query.Append("Publisher=@Publisher AND");
                    parameters.Add(new SqlParameter("@Publisher", book.Publisher));
                }
                if (book.YearPublished != null)
                {
                    query.Append("YearPublished=@YearPublished AND");
                    parameters.Add(new SqlParameter("@YearPublished", book.YearPublished));
                }
                query.Remove(query.Length - 1, 4);
                SqlCommand cmd= new SqlCommand(query.ToString(), connection);
                cmd.Parameters.AddRange(parameters.ToArray());
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
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
            }
            return books;
        }
    }
}
