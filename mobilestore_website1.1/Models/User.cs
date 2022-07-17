using System;
using System.Collections.Generic;

namespace mobilestore_website1._1.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserUsername { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string? UserAdmin { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? UserStreet { get; set; }
        public string? UserCountry { get; set; }
        public string? UserCity { get; set; }
        public string? UserBuilding { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
