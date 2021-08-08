using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models.Database.Entities
{
    class UserEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]public RoleEntity Role { get; set; }
    }
}
