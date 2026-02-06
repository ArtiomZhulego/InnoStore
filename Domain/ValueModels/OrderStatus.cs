namespace Domain.ValueModels;

public enum OrderStatus : byte
{
    Created = 0,
    Canceled = 1,
    Completed = 2,
}