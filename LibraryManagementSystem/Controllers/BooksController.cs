using LibraryManagementSystem.Model;
using LibraryManagementSystem.Model.DTOs;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookRepository.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            try
            {
                var book = await _bookRepository.GetBooksById(bookId);
                if (book == null)
                {
                    return NotFound($"Book not found with id {bookId}");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddNewBook")]
        public async Task<IActionResult> AddNewBook(Book book)
        {
            try
            {
                var res = await _bookRepository.AddNewBook(book);
                if (res)
                {
                    return Ok("New book added successfully");
                }
                return BadRequest("Invalid input");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            try
            {
                var res = await _bookRepository.UpdateBook(book);
                if (res)
                {
                    return Ok("Book details updated successfully");
                }
                return BadRequest("Invalid data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            try
            {
                var res = await _bookRepository.DeleteBook(bookId);
                if (res)
                {
                    return Ok("Book deleted successfully");
                }
                return NotFound($"Book not found with {bookId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("SearchBooks")]
        public async Task<IActionResult> SearchBooks(BookDto book)
        {
            try
            {
                var books = await _bookRepository.SearchBooks(book);
                return Ok(books);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
