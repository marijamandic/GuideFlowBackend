using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Follower : Entity
    {
        public long UserId { get; set; }
        public int FollowerId { get; private set; }
        public string FollowerUsername { get; private set; }
        public Follower() { }
        public Follower(int userId, int followerId, string followerUsername)
        {
            UserId = userId;
            FollowerId = followerId;
            FollowerUsername = followerUsername;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(FollowerUsername)) throw new ArgumentException("Invalid Name");
            if (UserId<1) throw new ArgumentException("Invalid user");
            if (FollowerId < 1) throw new ArgumentException("Invalid follower");
        }
    }
}
