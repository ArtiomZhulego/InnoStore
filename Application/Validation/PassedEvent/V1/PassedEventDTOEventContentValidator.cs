using Application.Abstractions.DTOs.Entities.PassedEvent.V1;
using FluentValidation;

namespace Application.Validation.PassedEvent.V1;

public class PassedEventDTOEventContentValidator : AbstractValidator<PassedEventDTOEventContent>
{
    public PassedEventDTOEventContentValidator()
    {
        this.RuleFor(x => x.Speakers)
            .Must(x => !HaveDuplicates(x))
            .WithMessage(x => $"Invalid event payload: content has duplicated speakers.");

        this.RuleFor(x => x.Assistents)
            .Must(x => !HaveDuplicates(x))
            .WithMessage(x => $"Invalid event payload: content has duplicated assistents.");
    }

    private bool HaveDuplicates(IEnumerable<PassedEventDTOEventContentSpeaker> speakers)
    {
        var hrmIdSet = new HashSet<int>();

        foreach (var speaker in speakers)
        {
            if (!hrmIdSet.Add(speaker.HrmId))
            {
                return true;
            }
        }
        return false;
    }

    private bool HaveDuplicates(IEnumerable<PassedEventDTOEventContentAssistent> assistents)
    {
        var hrmIdSet = new HashSet<int>();

        foreach (var assistent in assistents)
        {
            if (!hrmIdSet.Add(assistent.HrmId))
            {
                return true;
            }
        }
        return false;
    }
}
