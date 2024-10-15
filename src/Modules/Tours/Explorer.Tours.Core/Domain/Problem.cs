using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Problem : Entity
    {
        public long UserId { get; init; }
        public long TourId { get; init; }
        // ne moze string
        public string Category { get; private set; }
        // ne moze string
        public string Priority { get; private set; }
        public string Description { get; private set; }
        public DateOnly ReportedAt { get; private set; }

        public Problem(string category, string priority, string description, DateOnly reportedAt)
        {
            Category = category;
            Priority = priority;
            Description = description;
            ReportedAt = reportedAt;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Category)) throw new ArgumentException("Invalid category");
            if (string.IsNullOrWhiteSpace(Priority)) throw new ArgumentException("Invalid priority");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid description");
            if (ReportedAt < DateOnly.FromDateTime(DateTime.Today)) throw new ArgumentException("Invalid report date");
        }
    }
}
