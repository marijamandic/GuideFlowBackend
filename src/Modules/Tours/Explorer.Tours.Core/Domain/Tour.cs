using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public int Level { get; set; }
        public List<int> Taggs { get; set; }
        public string Status { get; set; }

        public int Price { get; set; }
        public Tour() {
            this.Status = "draft";
            this.Price = 0;
        }
        public Tour(string name, string? description, int level, List<int> taggs)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name.");

            Name = name;
            Description = description;
            Level = level;
            Taggs = taggs ?? new List<int>(); 
            Status = "draft";
            Price = 0;
        }

    }
}
