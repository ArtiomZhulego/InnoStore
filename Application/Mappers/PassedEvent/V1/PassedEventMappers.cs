using Application.Abstractions.DTOs.Entities.PassedEvent.V1;
using Application.Mappers;
using Application.Mappers.PassedEvent.V1;
using Domain.Entities;

namespace Application.Mappers.PassedEvent.V1;

internal static class PassedEventMappers
{
    extension(PassedEventDTO passedEventDTO)
    {
        public Domain.Entities.PassedEvent ToPassedEvent()
        {
            var passedEventId = Guid.NewGuid();
            return new Domain.Entities.PassedEvent
            {
                Id = passedEventId,
                EventId = passedEventDTO.EventContent.Id,
                EventType = passedEventDTO.EventContent.Type.ToPassedEventType(),
                Name = passedEventDTO.EventContent.Title,
                Participants = passedEventDTO.EventContent.GetPassedEventParticipants(passedEventId),
            };
        }
    }

    extension(PassedEventDTOEventContent eventContent)
    {
        public PassedEventParticipant[] GetPassedEventParticipants(Guid passedEventId)
        {
            var participants = new List<PassedEventParticipant>();

            var speakers = eventContent.Speakers
                .Select(x => x.ToPassedEventParticipant(passedEventId))
                .ToArray();

            var assistents = eventContent.Assistents
                .Select(x => x.ToPassedEventParticipant(passedEventId))
                .ToArray();

            participants.AddRange(speakers);
            participants.AddRange(assistents);

            return participants.ToArray();
        }
    }

    extension(PassedEventDTOEventContentSpeaker speaker)
    {
        public PassedEventParticipant ToPassedEventParticipant(Guid passedEventId)
        {
            return new PassedEventParticipant
            {
                HrmId = speaker.HrmId,
                Role = PassedEventParticipantRole.Speaker,
                PassedEventId = passedEventId,
            };
        }
    }

    extension(PassedEventDTOEventContentAssistent assistent)
    {
        public PassedEventParticipant ToPassedEventParticipant(Guid passedEventId)
        {
            return new PassedEventParticipant
            {
                HrmId = assistent.HrmId,
                Role = PassedEventParticipantRole.Assistent,
                PassedEventId = passedEventId,
            };
        }
    }

    extension(PassedEventDTOEventContentEventType passedEventDTOType)
    {
        public PassedEventType ToPassedEventType()
        {
            return passedEventDTOType switch
            {
                PassedEventDTOEventContentEventType.Unspecified => PassedEventType.Unspecified,
                PassedEventDTOEventContentEventType.OpenMeetUp => PassedEventType.OpenMeetUp,
                PassedEventDTOEventContentEventType.InternalMeetUp => PassedEventType.InternalMeetUp,
                PassedEventDTOEventContentEventType.UniversityMeetUp => PassedEventType.UniversityMeetUp,

                _ => throw new ArgumentOutOfRangeException(nameof(passedEventDTOType), passedEventDTOType, $"Mapping isn't specified for {nameof(PassedEventDTOEventContentEventType)} for value: {passedEventDTOType.ToString()}")
            };
        }
    }
}
