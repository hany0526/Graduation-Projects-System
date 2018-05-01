namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ideas", new[] { "leader_Id" });
            DropIndex("dbo.Ideas", new[] { "professor1_Id" });
            DropIndex("dbo.Ideas", new[] { "professor2_Id" });
            DropIndex("dbo.Ideas", new[] { "professor3_Id" });
            DropColumn("dbo.Ideas", "leaderid");
            DropColumn("dbo.Ideas", "professor1id");
            DropColumn("dbo.Ideas", "professor2id");
            DropColumn("dbo.Ideas", "professor3id");
            RenameColumn(table: "dbo.Ideas", name: "leader_Id", newName: "leaderid");
            RenameColumn(table: "dbo.Ideas", name: "professor1_Id", newName: "professor1id");
            RenameColumn(table: "dbo.Ideas", name: "professor2_Id", newName: "professor2id");
            RenameColumn(table: "dbo.Ideas", name: "professor3_Id", newName: "professor3id");
            AlterColumn("dbo.Ideas", "leaderid", c => c.String(maxLength: 128));
            AlterColumn("dbo.Ideas", "professor1id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Ideas", "professor2id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Ideas", "professor3id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Ideas", "leaderid");
            CreateIndex("dbo.Ideas", "professor1id");
            CreateIndex("dbo.Ideas", "professor2id");
            CreateIndex("dbo.Ideas", "professor3id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ideas", new[] { "professor3id" });
            DropIndex("dbo.Ideas", new[] { "professor2id" });
            DropIndex("dbo.Ideas", new[] { "professor1id" });
            DropIndex("dbo.Ideas", new[] { "leaderid" });
            AlterColumn("dbo.Ideas", "professor3id", c => c.Int(nullable: false));
            AlterColumn("dbo.Ideas", "professor2id", c => c.Int(nullable: false));
            AlterColumn("dbo.Ideas", "professor1id", c => c.Int(nullable: false));
            AlterColumn("dbo.Ideas", "leaderid", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Ideas", name: "professor3id", newName: "professor3_Id");
            RenameColumn(table: "dbo.Ideas", name: "professor2id", newName: "professor2_Id");
            RenameColumn(table: "dbo.Ideas", name: "professor1id", newName: "professor1_Id");
            RenameColumn(table: "dbo.Ideas", name: "leaderid", newName: "leader_Id");
            AddColumn("dbo.Ideas", "professor3id", c => c.Int(nullable: false));
            AddColumn("dbo.Ideas", "professor2id", c => c.Int(nullable: false));
            AddColumn("dbo.Ideas", "professor1id", c => c.Int(nullable: false));
            AddColumn("dbo.Ideas", "leaderid", c => c.Int(nullable: false));
            CreateIndex("dbo.Ideas", "professor3_Id");
            CreateIndex("dbo.Ideas", "professor2_Id");
            CreateIndex("dbo.Ideas", "professor1_Id");
            CreateIndex("dbo.Ideas", "leader_Id");
        }
    }
}
