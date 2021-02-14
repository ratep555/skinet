namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        //tis is the information we gonna want to display on a navbar in our angular application
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }
}