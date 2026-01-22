namespace Application.Abstractions.DTOs.Clients.HRM;

public sealed class EmailFilter
{
    public new string? Equals { get; set; }

    public string? NotEquals { get; set; }

    public bool? Specified { get; set; }

    public string[]? In { get; set; }

    public string[]? NotIn { get; set; }

    public string? Contains { get; set; }

    public string? DoesNotContain { get; set; }
}
