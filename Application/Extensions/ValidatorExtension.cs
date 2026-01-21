using Domain.Exceptions;
using FluentValidation;

namespace Application.Extensions;

public static class ValidatorExtension
{
    public static async Task EnsureValidAsync<T>(this IValidator<T> validator, T model, CancellationToken cancellationToken = default)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);

        if(result.IsValid)
        {
            return;
        }

        var errorMessage = string.Join("/n", result.Errors.Select(x => x.ErrorMessage));

        throw new GeneralValidationException(errorMessage);
    }
}
