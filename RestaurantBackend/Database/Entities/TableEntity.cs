using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models.Database.Entities
{
    class TableEntity:BaseEntity
    {
        public int NoOfSeats { get; set; }
        public int NoOfOccupiedSeats { get; set; }
        public List<OrderEntity> Orders { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]public UserEntity User { get; set; }

    }
}
