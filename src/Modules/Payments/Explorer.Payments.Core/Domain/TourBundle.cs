using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public  class TourBundle: Entity
    {
        public string Name { get; private set; }

        public double Price { get; private set; }

        public BundleStatus Status { get; private set; }

        public long AuthorId { get; private set; }

        public List<long> TourIds {  get; private set; } = new List<long>();

        public TourBundle(string name, double price, BundleStatus status, long authorId, List<long> tourIds)
        {
            Name = name;
            Price = price;
            Status = status;
            AuthorId = authorId;
            TourIds = tourIds;
            Validate();
        }

        public void Publish()
        {
            if (Status != BundleStatus.Draft) throw new InvalidOperationException("Cannot Publish Tour Bundle That Isn't Draft");
            Status = BundleStatus.Published;
        }

        public void Archive()
        {
            if (Status != BundleStatus.Published) throw new InvalidOperationException("Cannot Archive Tour Bundle That Isn't Published");
            Status = BundleStatus.Archived;
        }

        public void Validate()
        {
            if (!Enum.IsDefined(typeof(BundleStatus), Status)) throw new ArgumentException("Invalid Bundle Status");
        }
    }   

    public enum BundleStatus
    {
        Draft,
        Published,
        Archived
    }
}
