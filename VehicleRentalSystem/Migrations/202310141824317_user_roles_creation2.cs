namespace VehicleRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_roles_creation2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoleMappings",
                c => new
                    {
                        UserRoleMappingId = c.Int(nullable: false, identity: true),
                        Role_Id = c.Int(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.UserRoleMappingId)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleMappings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoleMappings", "Role_Id", "dbo.Roles");
            DropIndex("dbo.UserRoleMappings", new[] { "User_Id" });
            DropIndex("dbo.UserRoleMappings", new[] { "Role_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoleMappings");
            DropTable("dbo.Roles");
        }
    }
}
