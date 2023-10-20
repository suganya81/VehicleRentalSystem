namespace VehicleRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_roles_update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoleMappings", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoleMappings", "User_Id", "dbo.Users");
            DropIndex("dbo.UserRoleMappings", new[] { "Role_Id" });
            DropIndex("dbo.UserRoleMappings", new[] { "User_Id" });
            RenameColumn(table: "dbo.UserRoleMappings", name: "Role_Id", newName: "RoleId");
            RenameColumn(table: "dbo.UserRoleMappings", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.UserRoleMappings", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRoleMappings", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.UserRoleMappings", "UserId");
            CreateIndex("dbo.UserRoleMappings", "RoleId");
            AddForeignKey("dbo.UserRoleMappings", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleMappings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleMappings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoleMappings", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserRoleMappings", new[] { "RoleId" });
            DropIndex("dbo.UserRoleMappings", new[] { "UserId" });
            AlterColumn("dbo.UserRoleMappings", "UserId", c => c.Guid());
            AlterColumn("dbo.UserRoleMappings", "RoleId", c => c.Int());
            RenameColumn(table: "dbo.UserRoleMappings", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.UserRoleMappings", name: "RoleId", newName: "Role_Id");
            CreateIndex("dbo.UserRoleMappings", "User_Id");
            CreateIndex("dbo.UserRoleMappings", "Role_Id");
            AddForeignKey("dbo.UserRoleMappings", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoleMappings", "Role_Id", "dbo.Roles", "Id");
        }
    }
}
