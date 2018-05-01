namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Departmentid", "dbo.Departments");
            DropForeignKey("dbo.AspNetUsers", "leaderid", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Departmentid" });
            DropIndex("dbo.AspNetUsers", new[] { "leaderid" });
            DropColumn("dbo.AspNetUsers", "Departmentid");
            DropColumn("dbo.AspNetUsers", "leaderid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "leaderid", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Departmentid", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "leaderid");
            CreateIndex("dbo.AspNetUsers", "Departmentid");
            AddForeignKey("dbo.AspNetUsers", "leaderid", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Departmentid", "dbo.Departments", "id", cascadeDelete: true);
        }
    }
}
