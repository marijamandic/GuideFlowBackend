using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    [JsonDerivedType(typeof(UserDto), typeDiscriminator: "admin")]
    [JsonDerivedType(typeof(TouristDto), typeDiscriminator: "turista")]
    [JsonDerivedType(typeof(AuthorDto), typeDiscriminator: "autor")]
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }    
        public string Password { get; set; }    
        public UserRole Role { get; set; }
        public LocationDto Location { get; set; }
        public bool? IsActive { get; set; } = true;
    }

}
