namespace TechnicalTest.Domain.Entities;

public class Organization
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    private Organization() { }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

    public Organization(string name, string slugTenant, string connectionString)
    {
        Name = name;
        SlugTenant = slugTenant;
        ConnectionString = connectionString;
    }

    public int Id { get; set; }
    public string Name { get; private set; }
    public string SlugTenant { get; private set; }
    public string ConnectionString { get; private set; }
}
