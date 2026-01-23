using Application.Abstractions.DTOs.Entities.PassedEvent.V1;
using FluentValidation;

namespace Application.Validation.PassedEvent.V1;

public class PassedEventDTOValidator : AbstractValidator<PassedEventDTO>
{
    public PassedEventDTOValidator()
    {
        this.RuleFor(x => x.EventContent)
            .SetValidator(new PassedEventDTOEventContentValidator());
    }
}
