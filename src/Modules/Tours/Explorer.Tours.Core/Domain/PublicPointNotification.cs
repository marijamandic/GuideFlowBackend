using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicPointNotification : Entity
    {
        public int PublicPointId { get; private set; }
        public int AuthorId { get; private set; }
        public bool IsAccepted { get; private set; }
        public string Comment { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreationTime { get; private set; }

        public PublicPointNotification(int publicPointId, int authorId, bool isAccepted, string comment = "", DateTime creationTime = default)
        {
            PublicPointId = publicPointId;
            AuthorId = authorId;
            IsAccepted = isAccepted;
            Comment = comment;
            IsRead = false;
            CreationTime = creationTime;
        }

        public PublicPointNotification()
        {

        }

        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
