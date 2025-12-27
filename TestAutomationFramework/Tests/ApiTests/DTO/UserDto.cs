public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Website { get; set; } = null!;
    public CompanyDto Company { get; set; } = null!;
}
