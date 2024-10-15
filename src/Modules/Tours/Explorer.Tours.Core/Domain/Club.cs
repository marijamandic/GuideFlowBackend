using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Club : Entity
    {
        public long ClubId { get; set; }
        public long OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Club(long clubId,long ownerId,string name,string description,string imageUrl)
        {
            ClubId = clubId;
            OwnerId = ownerId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
    

}
