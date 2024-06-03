using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRepository _borrowRepository;
        public BorrowController(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }
        [HttpGet("GetAllBorrows")]
        public async Task<IActionResult> GetAllBorrows()
        {
            try
            {
                var borrows = await _borrowRepository.GetAllBorrows();
                return Ok(borrows);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("BorrowBook")]
        public async Task<IActionResult> BorrowBook(Borrow borrowDetails)
        {
            try
            {
                var res = await _borrowRepository.BorrowBook(borrowDetails);
                if (res)
                {
                    return Ok("book borrowed successfully");
                }
                return BadRequest("invalid details");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("ReturnBook")]
        public async Task<IActionResult> ReturnBook(int borrowId)
        {
            try
            {
                var res = await _borrowRepository.ReturnBook(borrowId);
                if (res)
                {
                    return Ok("book returned successfully");
                }
                return NotFound("borrow not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
