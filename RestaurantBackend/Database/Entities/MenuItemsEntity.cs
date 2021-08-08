using System.Collections.Generic;

namespace Restaurant.Models.Database.Entities
{
    class MenuItemsEntity:BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public List<OrderEntity> Orders { get; set; }
    }
}
