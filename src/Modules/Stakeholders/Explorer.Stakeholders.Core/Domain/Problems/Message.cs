using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Problems;
public class Message : Entity
{
    public long ProblemId { get; private set; }
    public long UserId { get; private set; }
    public string Content { get; private set; }
    public DateTime PostedAt { get; private set; }

    public Message(long problemId, long userId, string content, DateTime postedAt)
    {
        ProblemId = problemId;
        UserId = userId;
        Content = content;
        PostedAt = postedAt;
        //Validate();
    }

    //Validate()
}
