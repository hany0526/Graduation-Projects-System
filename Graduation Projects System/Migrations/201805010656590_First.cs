namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        leaderid = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        tools = c.String(nullable: false),
                        professor1id = c.Int(nullable: false),
                        professor2id = c.Int(nullable: false),
                        professor3id = c.Int(nullable: false),
                        isApproved = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Students", t => t.leaderid, cascadeDelete: false)
                .ForeignKey("dbo.Professors", t => t.professor1id, cascadeDelete: false)
                .ForeignKey("dbo.Professors", t => t.professor2id, cascadeDelete: false)
                .ForeignKey("dbo.Professors", t => t.professor3id, cascadeDelete: false)
                .Index(t => t.leaderid)
                .Index(t => t.professor1id)
                .Index(t => t.professor2id)
                .Index(t => t.professor3id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        Departmentid = c.Int(nullable: false),
                        level = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                        skills = c.String(nullable: false),
                        phone = c.Int(nullable: false),
                        leaderid = c.Int(nullable: false),
                        file = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Departments", t => t.Departmentid, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.leaderid)
                .Index(t => t.Departmentid)
                .Index(t => t.leaderid);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Departmentid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Departments", t => t.Departmentid, cascadeDelete: true)
                .Index(t => t.Departmentid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ideas", "professor3id", "dbo.Professors");
            DropForeignKey("dbo.Ideas", "professor2id", "dbo.Professors");
            DropForeignKey("dbo.Ideas", "professor1id", "dbo.Professors");
            DropForeignKey("dbo.Professors", "Departmentid", "dbo.Departments");
            DropForeignKey("dbo.Ideas", "leaderid", "dbo.Students");
            DropForeignKey("dbo.Students", "leaderid", "dbo.Students");
            DropForeignKey("dbo.Students", "Departmentid", "dbo.Departments");
            DropIndex("dbo.Professors", new[] { "Departmentid" });
            DropIndex("dbo.Students", new[] { "leaderid" });
            DropIndex("dbo.Students", new[] { "Departmentid" });
            DropIndex("dbo.Ideas", new[] { "professor3id" });
            DropIndex("dbo.Ideas", new[] { "professor2id" });
            DropIndex("dbo.Ideas", new[] { "professor1id" });
            DropIndex("dbo.Ideas", new[] { "leaderid" });
            DropTable("dbo.Professors");
            DropTable("dbo.Students");
            DropTable("dbo.Ideas");
            DropTable("dbo.Departments");
            DropTable("dbo.Admins");
        }
    }
}
