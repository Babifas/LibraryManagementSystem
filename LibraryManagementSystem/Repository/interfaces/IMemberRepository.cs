using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Repository.interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllMembers();
        Task<Member> GetMemberById(int memberId);
        Task<bool> Register(Member member);
        Task<bool> UpdateMember(Member member);
        Task<bool> DeleteMember(int memberId);
    }
}
