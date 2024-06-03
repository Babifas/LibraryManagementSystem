using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            try
            {
                var members=await _memberRepository.GetAllMembers();
                return Ok(members);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetMemberById")]
        public async Task<IActionResult> GetMemberById(int memberId)
        {
            try
            {
                var member = await _memberRepository.GetMemberById(memberId);
                if(member == null)
                {
                    return NotFound($"member not found with id {memberId}");
                }
                return Ok(member);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Member member)
        {
            try
            {
                var res = await _memberRepository.Register(member);
                if (res)
                {
                    return Ok("registered successfully");
                }
                return BadRequest("Invalid data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateMember")]
        public async Task<IActionResult> UpdateMember(Member member)
        {
            try
            {
                var res = await _memberRepository.UpdateMember(member);
                if (res)
                {
                    return Ok("member details updated successfully");
                }
                return BadRequest("Invalid data");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteMember")]
        public async Task<IActionResult> DeleteMember(int memberId)
        {
            try
            {
                var res = await _memberRepository.DeleteMember(memberId);
                if (res)
                {
                    return Ok("member deleted successfully");
                }
                return NotFound($"member not found with id {memberId}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
