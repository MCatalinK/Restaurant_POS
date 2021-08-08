using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models.Database.Entities
{
    class OrderEntity : BaseEntity
    {
        public int ItemId { get; set; }
        [ForeignKey("ItemId")] public MenuItemsEntity Item { get; set; }
        public int TableId { get; set; }
        [ForeignKey("TableId")] public TableEntity Table { get; set; }
    }
}
