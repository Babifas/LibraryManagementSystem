namespace LibraryManagementSystem.Model
{
    public class Member
    {
        public int MemberId {  get; set; }
        public string FastName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }
        public string Address { get; set; } 
        public DateTime DateOfMembership {  get; set; }

    }
}
