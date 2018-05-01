namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ideas", "leaderid", "dbo.Students");
            DropForeignKey("dbo.Ideas", "professor1id", "dbo.Professors");
            DropForeignKey("dbo.Ideas", "professor2id", "dbo.Professors");
            DropForeignKey("dbo.Ideas", "professor3id", "dbo.Professors");
            DropIndex("dbo.Ideas", new[] { "leaderid" });
            DropIndex("dbo.Ideas", new[] { "professor1id" });
            DropIndex("dbo.Ideas", new[] { "professor2id" });
            DropIndex("dbo.Ideas", new[] { "professor3id" });
            AddColumn("dbo.Ideas", "leader_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Ideas", "professor1_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Ideas", "professor2_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Ideas", "professor3_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ideas", "leader_Id");
            CreateIndex("dbo.Ideas", "professor1_Id");
            CreateIndex("dbo.Ideas", "professor2_Id");
            CreateIndex("dbo.Ideas", "professor3_Id");
            AddForeignKey("dbo.Ideas", "leader_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ideas", "professor1_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ideas", "professor2_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Ideas", "professor3_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "professor3_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor2_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "professor1_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ideas", "leader_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ideas", new[] { "professor3_Id" });
            DropIndex("dbo.Ideas", new[] { "professor2_Id" });
            DropIndex("dbo.Ideas", new[] { "professor1_Id" });
            DropIndex("dbo.Ideas", new[] { "leader_Id" });
            DropColumn("dbo.Ideas", "professor3_Id");
            DropColumn("dbo.Ideas", "professor2_Id");
            DropColumn("dbo.Ideas", "professor1_Id");
            DropColumn("dbo.Ideas", "leader_Id");
            CreateIndex("dbo.Ideas", "professor3id");
            CreateIndex("dbo.Ideas", "professor2id");
            CreateIndex("dbo.Ideas", "professor1id");
            CreateIndex("dbo.Ideas", "leaderid");
            AddForeignKey("dbo.Ideas", "professor3id", "dbo.Professors", "id", cascadeDelete: true);
            AddForeignKey("dbo.Ideas", "professor2id", "dbo.Professors", "id", cascadeDelete: true);
            AddForeignKey("dbo.Ideas", "professor1id", "dbo.Professors", "id", cascadeDelete: true);
            AddForeignKey("dbo.Ideas", "leaderid", "dbo.Students", "id", cascadeDelete: true);
        }
    }
}
