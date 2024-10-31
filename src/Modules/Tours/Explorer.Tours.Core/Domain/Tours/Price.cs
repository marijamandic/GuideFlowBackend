using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Price : ValueObject<Price>
    {
        public double Cost;
        public  Currency Currency;

        public Price() 
        {
            Cost = 0;
            Currency = Currency.EUR;
        }

        public Price(Price price)
        {
            this.Cost = price.Cost;
            this.Currency = price.Currency;
        }

        public void Validate()
        {
            //TODO
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
        EUR        
    }

}
