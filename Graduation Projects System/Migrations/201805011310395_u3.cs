namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class u3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "leaderid", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "leaderid" });
            AddColumn("dbo.AspNetUsers", "leaderid", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "leaderid");
            AddForeignKey("dbo.AspNetUsers", "leaderid", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Students", "leaderid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "leaderid", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "leaderid", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "leaderid" });
            DropColumn("dbo.AspNetUsers", "leaderid");
            CreateIndex("dbo.Students", "leaderid");
            AddForeignKey("dbo.Students", "leaderid", "dbo.AspNetUsers", "Id");
        }
    }
}
