namespace LibraryManagementSystem.Model
{
    public class Borrow
    {
        public int BorrowId { get; set; }
        public int BookId {  get; set; }
        public int MemberId {  get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set;}
        public DateTime? ReturnDate { get; set; }
        public string Status {  get; set; }
    }
}
