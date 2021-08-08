using Restaurant.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Database
{
    class RestaurantDbContext:DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<TableEntity> Tables { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<TicketTypeEntity> TicketTypes { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<MenuItemsEntity> MenuItems { get; set; }
    }
}
