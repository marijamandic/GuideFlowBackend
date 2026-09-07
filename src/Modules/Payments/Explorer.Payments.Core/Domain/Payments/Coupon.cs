using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.Payments
{
    public class Coupon : Entity
    {

        public long AuthorId { get; private set; }
        public long? TourId { get; private set; }
        public string Code { get; private set; }
        public double Discount { get; private set; }
        public DateTime? ExpiryDate { get; private set; }
        public bool ValidForAllTours { get; private set; }
        public bool Redeemed { get; private set; }

        public Coupon(long AuthorId, long? TourId, string Code, double Discount, DateTime? ExpiryDate, bool ValidForAllTours)
        {
            this.AuthorId = AuthorId;
            this.TourId = TourId;
            this.Code = Code;
            this.Discount = Discount;
            this.ExpiryDate = ExpiryDate;
            this.ValidForAllTours = ValidForAllTours;
            this.Redeemed = false;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Code) || Code.Length != 8)
                throw new ArgumentException("Coupon code must be 8 characters long.");
            if (Discount <= 0 || Discount > 100)
                throw new ArgumentException("Discount must be greater than 0 and less than or equal to 100.");
            if (ValidForAllTours && TourId.HasValue)
                throw new InvalidOperationException("A coupon valid for all tours cannot be linked to a specific tour.");

        }

        public void Redeem()
        {
            if (Redeemed)
                throw new InvalidOperationException("Coupon has already been redeemed.");
            if (ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow)
                throw new InvalidOperationException("Coupon has expired.");

            Redeemed = true;
        }

        public bool IsValidForTour(long tourId)
        {
            if (Redeemed)
                return false;
            if (ExpiryDate.HasValue && ExpiryDate.Value < DateTime.UtcNow)
                return false;
            if (!ValidForAllTours && TourId != tourId)
                return false;

            return true;
        }

        public void UpdateDetails(long? tourId, string code, double discount, DateTime? expiryDate, bool validForAllTours)
        {
            TourId = tourId;
            Code = code;
            Discount = discount;
            ExpiryDate = expiryDate;
            ValidForAllTours = validForAllTours;
        }


    }
}
