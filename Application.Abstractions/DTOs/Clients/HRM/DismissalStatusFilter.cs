namespace Application.Abstractions.DTOs.Clients.HRM;

public sealed class DismissalStatusFilter
{
    public string Equals { get; set; }
    public string NotEquals { get; set; }
    public bool? Specified { get; set; }
    public string[] In { get; set; }
    public string[] NotIn { get; set; }
}