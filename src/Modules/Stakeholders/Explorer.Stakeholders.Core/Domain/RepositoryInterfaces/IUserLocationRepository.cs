using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IUserLocationRepository
    {
        void Add(UserLocation userLocation);
        UserLocation GetById(int id);
        void Update(UserLocation userLocation);

    }
}
