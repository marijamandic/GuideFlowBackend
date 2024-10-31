using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Price : ValueObject<Price>
    {
        public double Cost;
        public Currency Currency;

        [JsonConstructor]
        public Price(double cost,Currency currency)
        {
            this.Cost = cost;
            this.Currency = currency;
            Validate();
        }

        private void Validate()
        {
            if (Cost <= 0)
                throw new ArgumentException("Cost must be greater than zero.");

            if (!Enum.IsDefined(typeof(Currency), Currency))
                throw new ArgumentException("Invalid currency value.");
        }

        protected override bool EqualsCore(Price other)
        {
            return Cost == other.Cost &&
                    Currency == other.Currency;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Cost.GetHashCode();
                hashCode = (hashCode * 397) ^ Currency.GetHashCode();

                return hashCode;
            }
        }
    }

    public enum Currency 
    {
        RSD,
        EUR,
        USD
    }

}
