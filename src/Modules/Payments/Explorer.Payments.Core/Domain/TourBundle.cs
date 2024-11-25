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

        public Status Status { get; private set; }

        public long AuthorId { get; private set; }

        private List<long> _tourIds = new List<long>();

        public IReadOnlyList<long> TourIds => new List<long>(_tourIds);

        public TourBundle(string name, double price, Status status,long authorId) 
        {
            Name = name;
            Price = price;
            Status = status;
            AuthorId = authorId;
            Validate();
        }

        public void AddTour(long tourId) 
        {
            _tourIds.Add(tourId);
        }

        public void RemoveTour(long tourId)
        {
            if (_tourIds.Count == 0) throw new InvalidOperationException("Cannot Remove Anymore Tours");
            _tourIds.Remove(tourId);
        }

        public void Publish()
        {
            if (Status != Status.Draft) throw new InvalidOperationException("Cannot Publish Tour Bundle That Isn't Draft");
            Status = Status.Published;
        }

        public void Archive()
        {
            if (Status != Status.Published) throw new InvalidOperationException("Cannot Archive Tour Bundle That Isn't Published");
            Status = Status.Archived;
        }

        public void Validate()
        {
            if (!Enum.IsDefined(typeof(Status), Status)) throw new ArgumentException("Invalid Bundle Status");
        }
    }   

    public enum Status
    {
        Draft,
        Published,
        Archived
    }
}
