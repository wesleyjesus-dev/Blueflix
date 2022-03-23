namespace Blueflix.Application.Entities
{
    public class Employee : Person
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
