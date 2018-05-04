namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTablProfessors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Professors", "Departmentid", "dbo.Departments");
            DropIndex("dbo.Professors", new[] { "Departmentid" });
            AddColumn("dbo.Professors", "Interests", c => c.String(nullable: false));
            AddColumn("dbo.Professors", "userId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Professors", "userId");
            AddForeignKey("dbo.Professors", "userId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Professors", "name");
            DropColumn("dbo.Professors", "email");
            DropColumn("dbo.Professors", "password");
            DropColumn("dbo.Professors", "Phone");
            DropColumn("dbo.Professors", "Departmentid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Professors", "Departmentid", c => c.Int(nullable: false));
            AddColumn("dbo.Professors", "Phone", c => c.Int(nullable: false));
            AddColumn("dbo.Professors", "password", c => c.String(nullable: false));
            AddColumn("dbo.Professors", "email", c => c.String(nullable: false));
            AddColumn("dbo.Professors", "name", c => c.String(nullable: false));
            DropForeignKey("dbo.Professors", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.Professors", new[] { "userId" });
            DropColumn("dbo.Professors", "userId");
            DropColumn("dbo.Professors", "Interests");
            CreateIndex("dbo.Professors", "Departmentid");
            AddForeignKey("dbo.Professors", "Departmentid", "dbo.Departments", "id", cascadeDelete: true);
        }
    }
}
