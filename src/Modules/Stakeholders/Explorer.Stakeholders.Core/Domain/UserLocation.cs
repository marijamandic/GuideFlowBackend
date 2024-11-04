using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class UserLocation
    {
        public User User { get; private set; }          
        public Location Location { get; private set; }  

        public UserLocation(User user, Location location)
        {
            User = user;
            Location = location;
        }

        public void UpdateLocation(Location newLocation)
        {
            Location = newLocation;
        }
    }

}
