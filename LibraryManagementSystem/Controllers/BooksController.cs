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
                if(book == null)
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
    }
}
