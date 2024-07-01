namespace TaskerAPI.Model
{
    public class Users
    {
        public int ID { get; set; }  // Assuming Id is the primary key
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email_ID { get; set; }
        public string Password { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public bool? Is_Active { get; set; }
    }
}
