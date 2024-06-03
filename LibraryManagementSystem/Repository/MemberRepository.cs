using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository.interfaces;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Repository
{
    public class MemberRepository:IMemberRepository
    {
        private readonly string _connectionString;
        public MemberRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            List<Member> members = new List<Member>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Members",connection);
                SqlDataReader reader =await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    members.Add(new Member()
                    {
                        MemberId = Convert.ToInt32(reader["MemberId"]),
                        FastName = reader["FastName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        DateOfMembership = Convert.ToDateTime(reader["DateOfMembership"])
                    });
                }
            }
            return members;
        }
        public async Task<Member> GetMemberById(int memberId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Members WHERE MemberId=@MemberId",connection);
                cmd.Parameters.AddWithValue("@MemberId", memberId);
                SqlDataReader reader=await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Member
                    {
                        MemberId = Convert.ToInt32(reader["MemberId"]),
                        FastName = reader["FastName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        DateOfMembership = Convert.ToDateTime(reader["DateOfMembership"])
                    };
                }
                return null;
            }
        }
        public async Task<bool> Register(Member member)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("INSERT INTO Members(FastName,LastName,Email,Phone,Address,DateOfMembership) VALUES (@FastName,@LastName,@Email,@Phone,@Address,@DateOfMembership)",connection);
                cmd.Parameters.AddWithValue("@FastName", member.FastName);
                cmd.Parameters.AddWithValue("@LastName", member.LastName);
                cmd.Parameters.AddWithValue("@Email", member.Email);
                cmd.Parameters.AddWithValue("@Phone", member.Phone);
                cmd.Parameters.AddWithValue("@Address", member.Address);
                cmd.Parameters.AddWithValue("@DateOfMembership  ", DateTime.UtcNow);
                var rowAffected=await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        }
        public async Task<bool> UpdateMember(Member member)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("UPDATE Members SET FastName=@FastName,LastName=@LastName,Email=@Email,Phone=@Phone,Address=@Address WHERE MemberId=@MemberId",connection);
                cmd.Parameters.AddWithValue("@FastName", member.FastName);
                cmd.Parameters.AddWithValue("@LastName", member.LastName);
                cmd.Parameters.AddWithValue("@Email", member.Email);
                cmd.Parameters.AddWithValue("@Phone", member.Phone);
                cmd.Parameters.AddWithValue("@Address", member.Address);
                cmd.Parameters.AddWithValue("@MemberId", member.MemberId);
                var rowAffected = await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        }
        public async Task<bool> DeleteMember(int memberId)
        {
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("DELETE FROM Members WHERE MemberId=@MemberId", connection);
                cmd.Parameters.AddWithValue("@MemberId", memberId);
                var rowAffected=await cmd.ExecuteNonQueryAsync();
                return rowAffected > 0;
            }
        }
    }
}
