using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        //public long ClubId { get; init; }
        public long OwnerId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }

        public Club(long ownerId,string name,string description,string imageUrl)
        {
            //ClubId = clubId;
            OwnerId = ownerId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
    

}
