namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        tools = c.String(nullable: false),
                        professor1id = c.String(maxLength: 128),
                        professor2id = c.String(maxLength: 128),
                        professor3id = c.String(maxLength: 128),
                        isApproved = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.professor1id)
                .ForeignKey("dbo.AspNetUsers", t => t.professor2id)
                .ForeignKey("dbo.AspNetUsers", t => t.professor3id)
                .Index(t => t.professor1id)
                .Index(t => t.professor2id)
                .Index(t => t.professor3id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "professor3id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor2id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor1id", "dbo.AspNetUsers");
            DropIndex("dbo.Ideas", new[] { "professor3id" });
            DropIndex("dbo.Ideas", new[] { "professor2id" });
            DropIndex("dbo.Ideas", new[] { "professor1id" });
            DropTable("dbo.Ideas");
        }
    }
}
