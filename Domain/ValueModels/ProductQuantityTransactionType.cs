namespace Domain.ValueModels;

public enum ProductQuantityTransactionType : byte
{
    None = 0,
    Add = 1,
    Reduce = 2,
}