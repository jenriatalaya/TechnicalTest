namespace TechnicalTest.Domain.Entities;

public class User
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    private User() { }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public User(string username, string password, string email, int organizationId)
    {
        Username = username;
        Password = password;
        Email = email;
        OrganizationId = organizationId;
    }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
}