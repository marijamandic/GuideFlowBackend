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

        private List<long> _tourIds = new List<long>();

        public IReadOnlyList<long> TourIds => new List<long>(_tourIds);

        public TourBundle(string name, double price, Status status) 
        {
            Name = name;
            Price = price;
            Status = status;
            Validate();
        }

        public void AddTour(long tourId) 
        {
            _tourIds.Add(tourId);
        }

        public void RemoveTour(long tourId)
        {
            if (_tourIds.Count == 0) throw new ArgumentException("Cannot Remove Anymore Tours");
            _tourIds.Remove(tourId);
        }

        public void Validate()
        {
            if (Price <= 0) throw new ArgumentException("Invalid Bundle Price");
            if (!Enum.IsDefined(typeof(Status), Status)) throw new ArgumentException("Invalid Bundle Status");
        }
    }   

    public enum Status
    {
        Draft,
        Published,
        Archieved
    }
}
