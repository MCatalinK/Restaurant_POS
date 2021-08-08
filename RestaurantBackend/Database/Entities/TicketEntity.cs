using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models.Database.Entities
{
    class TicketEntity:BaseEntity
    {
        public float TotalPrice { get; set; } = 0f;
        public DateTime EmissionDate { get; set; } = DateTime.Now;
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]public TicketTypeEntity TicketType { get; set; }
    }
}
