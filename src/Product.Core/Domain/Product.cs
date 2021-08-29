using System;

namespace Product.Core.Domain
{
    public class Product
        : Entity
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
