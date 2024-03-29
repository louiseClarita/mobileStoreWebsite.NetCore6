﻿using System;
using System.Collections.Generic;

namespace mobilestore_website1._1.Models
{
    public partial class Model
    {
        public Model()
        {
            Products = new HashSet<Product>();
        }

        public int ModelId { get; set; }
        public string? ModelName { get; set; }
        public string? ModelVersion { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
