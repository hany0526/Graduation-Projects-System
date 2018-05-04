namespace Graduation_Projects_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTablProfessors1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Professors", "IsAproved", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Professors", "IsAproved");
        }
    }
}
