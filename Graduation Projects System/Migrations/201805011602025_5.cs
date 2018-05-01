namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ideas", "leaderid", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor1id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor2id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor3id", "dbo.AspNetUsers");
            DropIndex("dbo.Ideas", new[] { "leaderid" });
            DropIndex("dbo.Ideas", new[] { "professor1id" });
            DropIndex("dbo.Ideas", new[] { "professor2id" });
            DropIndex("dbo.Ideas", new[] { "professor3id" });
            DropTable("dbo.Ideas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        leaderid = c.String(maxLength: 128),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        tools = c.String(nullable: false),
                        professor1id = c.String(maxLength: 128),
                        professor2id = c.String(maxLength: 128),
                        professor3id = c.String(maxLength: 128),
                        isApproved = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.Ideas", "professor3id");
            CreateIndex("dbo.Ideas", "professor2id");
            CreateIndex("dbo.Ideas", "professor1id");
            CreateIndex("dbo.Ideas", "leaderid");
            AddForeignKey("dbo.Ideas", "professor3id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ideas", "professor2id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ideas", "professor1id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ideas", "leaderid", "dbo.AspNetUsers", "Id");
        }
    }
}
