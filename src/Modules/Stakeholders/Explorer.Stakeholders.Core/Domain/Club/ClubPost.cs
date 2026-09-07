using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Club
{
    public enum ResourceType
    {
        Tour,
        Blog
    }

    public class ClubPost : Entity
    {
        public long ClubId { get; private set; }
        public long MemberId { get; private set; }
        public string Content { get; private set; }
        public long? ResourceId { get; private set; }
        public ResourceType? ResourceType { get; private set; }

        // Constructor to initialize properties
        public ClubPost(long clubId, long memberId, string content, long? resourceId, ResourceType? resourceType)
        {
            ClubId = clubId;
            MemberId = memberId;
            Content = content;
            ResourceId = resourceId;
            ResourceType = resourceType;
        }
    }
}
