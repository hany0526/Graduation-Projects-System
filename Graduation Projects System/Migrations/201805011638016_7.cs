namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ideas", "leaderid", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ideas", "leaderid");
            AddForeignKey("dbo.Ideas", "leaderid", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "leaderid", "dbo.AspNetUsers");
            DropIndex("dbo.Ideas", new[] { "leaderid" });
            DropColumn("dbo.Ideas", "leaderid");
        }
    }
}
