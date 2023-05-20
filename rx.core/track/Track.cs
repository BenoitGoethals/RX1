using System.Runtime.CompilerServices;
using CoordinateSharp;

namespace rx.core.track;

public class Track
{
    public Guid IdGuid { get; } =Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SymbolCode { get; set; }

    public Coordinate? Location { get; set; }

    protected bool Equals(Track other)
    {
        return IdGuid.Equals(other.IdGuid);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Track)obj);
    }

    public override int GetHashCode()
    {
        return IdGuid.GetHashCode();
    }

    public static Track FromCsv(string? csvLine)
    {
        string?[]? values = csvLine?.Split(';');
        var track = new Track
        {
            Name = values?[0],
            Description = values?[0],
            Location = Coordinate.Parse(values?[1])
        };

        return track;
    }

}