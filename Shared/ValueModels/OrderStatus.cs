namespace Shared.ValueModels;

public enum OrderStatus : byte
{
    None = 0,
    Created = 1,
    Canceled = 2,
    Completed = 3,
}