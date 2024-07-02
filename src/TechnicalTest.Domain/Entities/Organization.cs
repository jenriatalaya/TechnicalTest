namespace TechnicalTest.Domain.Entities;

public class Organization
{
    public int Id { get; set; }
    public  required string Name { get; set; }
    public required string SlugTenant { get; set; }
    public required string ConnectionString { get; set; }
}
