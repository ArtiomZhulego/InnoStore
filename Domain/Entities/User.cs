namespace Domain.Entities;

public sealed class User
{
    public Guid? Id { get; init; }
    
    public required int HrmId { get; init; }

    public string? FirstNameRU { get; init; }

    public DateOnly? Birthdate { get; init; }

    public string? PatronymicRU { get; init; }

    public string? LastNameRU { get; init; }

    public string? FirstNameEN { get; init; }

    public string? PatronymicEN { get; init; }

    public string? LastNameEN { get; init; }

    public required string Email { get; init; }

    public int? OfficeId { get; init; }

    public Guid? JobTitleId { get; init; }

    public string? LinkProfilePictureMini { get; init; }

    public DateTime? CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }

    public List<Order> Orders { get; init; }
}