namespace DataMarket.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsEnabled { get; set; }
    }
}