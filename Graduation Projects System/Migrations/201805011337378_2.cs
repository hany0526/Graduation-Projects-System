namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Departmentid", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "leaderid", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "leaderid", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Departmentid", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "leaderid" });
            DropIndex("dbo.AspNetUsers", new[] { "Departmentid" });
            DropColumn("dbo.AspNetUsers", "leaderid");
            DropColumn("dbo.AspNetUsers", "Departmentid");
        }
    }
}
