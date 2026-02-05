using Application.Clients.HRM.Abstractions;
using Domain.Exceptions;
using FluentValidation;

namespace Application.Extensions;

public static class ValidatorExtension
{
    public static async Task EnsureValidAsync<TModel>(this IValidator validator, TModel model,
        CancellationToken cancellationToken = default)
    {
        if (!validator.CanValidateInstancesOfType(typeof(TModel)))
        {
            throw new InvalidOperationException(
                $"Validator of type {validator.GetType().Name} cannot validate model of type {typeof(TModel).Name}");
        }
        
        var context = ValidationContext<TModel>.CreateWithOptions(model, strategy => strategy.IncludeAllRuleSets());
        var result = await validator.ValidateAsync(context, cancellationToken);
        
        if (!result.IsValid)
        {
            var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(validationResult => validationResult.ErrorMessage));
            throw new GeneralValidationException(errorMessage);
        }
    }
    
    public static async Task EnsureValidAsync<T>(this IValidator<T> validator, T model, CancellationToken cancellationToken = default)
    {
        var result = await validator.ValidateAsync(model, cancellationToken);

        if (!result.IsValid)
        {
            var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(validationResult => validationResult.ErrorMessage));
            throw new GeneralValidationException(errorMessage);
        }
    }
}
