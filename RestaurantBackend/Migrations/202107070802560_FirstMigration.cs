namespace RestaurantBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemsEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        TableId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItemsEntities", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.TableEntities", t => t.TableId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.TableId);
            
            CreateTable(
                "dbo.TableEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoOfSeats = c.Int(nullable: false),
                        NoOfOccupiedSeats = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserEntities", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleEntities", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Single(nullable: false),
                        EmissionDate = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketTypeEntities", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.TicketTypeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketEntities", "TypeId", "dbo.TicketTypeEntities");
            DropForeignKey("dbo.TableEntities", "UserId", "dbo.UserEntities");
            DropForeignKey("dbo.UserEntities", "RoleId", "dbo.RoleEntities");
            DropForeignKey("dbo.OrderEntities", "TableId", "dbo.TableEntities");
            DropForeignKey("dbo.OrderEntities", "ItemId", "dbo.MenuItemsEntities");
            DropIndex("dbo.TicketEntities", new[] { "TypeId" });
            DropIndex("dbo.UserEntities", new[] { "RoleId" });
            DropIndex("dbo.TableEntities", new[] { "UserId" });
            DropIndex("dbo.OrderEntities", new[] { "TableId" });
            DropIndex("dbo.OrderEntities", new[] { "ItemId" });
            DropTable("dbo.TicketTypeEntities");
            DropTable("dbo.TicketEntities");
            DropTable("dbo.RoleEntities");
            DropTable("dbo.UserEntities");
            DropTable("dbo.TableEntities");
            DropTable("dbo.OrderEntities");
            DropTable("dbo.MenuItemsEntities");
        }
    }
}
