﻿using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Problems;

public class Problem : Entity
{
    private List<Message> _messages = new();

    public long UserId { get; init; }
    public long TourId { get; init; }
    public Details Details { get; private set; }
    public Resolution Resolution { get; private set; }
    public IReadOnlyList<Message> Messages => new List<Message>(_messages);

    public Problem(long id, long userId, long tourId)
    {
        Id = id;
        UserId = userId;
        TourId = tourId;
    }

    public void AddMessage (Message message)
    {
        if (message.ProblemId != Id) throw new Exception("Problem ID mismatch");
        message.Validate();

        _messages.Add(message);
    }
    public void ChangeStatus(string touristMessage, bool status)
    {
        Resolution.ChangeResolveStatus(status);
        var message = new Message(Id, UserId, touristMessage, DateTime.UtcNow);
        _messages.Add(message);
    }
    public void ChangeDeadline(DateTime deadline)
    {
        Resolution.SetDeadline(deadline);
    }
}