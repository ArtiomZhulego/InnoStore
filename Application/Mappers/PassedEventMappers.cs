using Application.Abstractions.DTOs.Entities;
using Domain.Entities;

namespace Application.Mappers;

internal static class PassedEventMappers
{
    extension(PassedEventDTO passedEventDTO)
    {
        public PassedEvent ToPassedEvent()
        {
            return new PassedEvent
            {
                Id = passedEventDTO.Id,
                EventType = passedEventDTO.EventType.ToPassedEventType(),
                Name = passedEventDTO.Name,
                Participants = passedEventDTO.Participants
                    .Select(x => x.ToPassedEventParticipant(passedEventDTO.Id))
                    .ToArray(),
            };
        }
    }

    extension(PassedEventParticipantDTO passedEventParticipantDTO)
    {
        public PassedEventParticipant ToPassedEventParticipant(Guid passedEventId)
        {
            return new PassedEventParticipant
            {
                HrmId = passedEventParticipantDTO.HrmId,
                Email = passedEventParticipantDTO.Email,
                Role = passedEventParticipantDTO.Role.ToPassedEventParticipantRole(),
                PassedEventId = passedEventId,
            };
        }
    }

    extension(PassedEventDTOType passedEventDTOType)
    {
        public PassedEventType ToPassedEventType()
        {
            return passedEventDTOType switch
            {
                PassedEventDTOType.Unspecified => PassedEventType.Unspecified,
                PassedEventDTOType.OpenMeetUp => PassedEventType.OpenMeetUp,
                PassedEventDTOType.InternalMeetUp => PassedEventType.InternalMeetUp,
                PassedEventDTOType.MasterClass => PassedEventType.MasterClass,

                _ => throw new ArgumentOutOfRangeException(nameof(passedEventDTOType), passedEventDTOType, $"Mapping isn't specified for {nameof(PassedEventDTOType)} for value: {passedEventDTOType.ToString()}")
            };
        }
    }

    extension(PassedEventParticipantDTORole passedEventParticipantDTORole)
    {
        public PassedEventParticipantRole ToPassedEventParticipantRole()
        {
            return passedEventParticipantDTORole switch
            {
                PassedEventParticipantDTORole.Unspecified => PassedEventParticipantRole.Unspecified,
                PassedEventParticipantDTORole.Speaker => PassedEventParticipantRole.Speaker,
                PassedEventParticipantDTORole.Organizer => PassedEventParticipantRole.Organizer,
                PassedEventParticipantDTORole.Attendee => PassedEventParticipantRole.Attendee,

                _ => throw new ArgumentOutOfRangeException(nameof(passedEventParticipantDTORole), passedEventParticipantDTORole, $"Mapping isn't specified for {nameof(PassedEventParticipantDTORole)} for value: {passedEventParticipantDTORole.ToString()}")
            };
        }
    }
}
