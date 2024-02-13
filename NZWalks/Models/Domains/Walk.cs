namespace NZWalks.Models.Domains;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImgUrl { get; set; }
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
    
    // Navigation properties
    // These are used to access the fields of the Difficulty/Region Id specified in the instance of the class.
    // EF Core automatically maps the object from the database to the objects below and then we can access their properties.
    public Difficulty Difficulty { get; set; }
    public Region Region { get; set; }
}