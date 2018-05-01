namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Departmentid", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "Departmentid" });
            //DropIndex("dbo.Students", new[] { "leaderid" });
            AddColumn("dbo.Students", "userId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Departmentid", c => c.Int(nullable: true));
            AddColumn("dbo.AspNetUsers", "file", c => c.String());
            //AlterColumn("dbo.Students", "leaderid", c => c.String(maxLength: 128));
            CreateIndex("dbo.Students", "userId");
            //CreateIndex("dbo.Students", "leaderid");
            CreateIndex("dbo.AspNetUsers", "Departmentid");
            AddForeignKey("dbo.AspNetUsers", "Departmentid", "dbo.Departments", "id", cascadeDelete: false);
            AddForeignKey("dbo.Students", "userId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Students", "type");
            DropColumn("dbo.Students", "name");
            DropColumn("dbo.Students", "email");
            DropColumn("dbo.Students", "password");
            DropColumn("dbo.Students", "Departmentid");
            DropColumn("dbo.Students", "phone");
            DropTable("dbo.Admins");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.Students", "phone", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Departmentid", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "password", c => c.String(nullable: false));
            AddColumn("dbo.Students", "email", c => c.String(nullable: false));
            AddColumn("dbo.Students", "name", c => c.String(nullable: false));
            AddColumn("dbo.Students", "type", c => c.Int(nullable: false));
            DropForeignKey("dbo.Students", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Departmentid", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "Departmentid" });
            DropIndex("dbo.Students", new[] { "leaderid" });
            DropIndex("dbo.Students", new[] { "userId" });
            AlterColumn("dbo.Students", "leaderid", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "file");
            DropColumn("dbo.AspNetUsers", "Departmentid");
            DropColumn("dbo.Students", "userId");
            CreateIndex("dbo.Students", "leaderid");
            CreateIndex("dbo.Students", "Departmentid");
            AddForeignKey("dbo.Students", "Departmentid", "dbo.Departments", "id", cascadeDelete: true);
        }
    }
}
