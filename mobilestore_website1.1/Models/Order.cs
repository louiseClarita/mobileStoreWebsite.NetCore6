using System;
using System.Collections.Generic;

namespace mobilestore_website1._1.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string? OrderDate { get; set; }
        public string? OrderBalance { get; set; }
        public string? OrderStreet { get; set; }
        public string? OrderCity { get; set; }
        public string? OrderBuilding { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderDeliveredData { get; set; }
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
    

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
