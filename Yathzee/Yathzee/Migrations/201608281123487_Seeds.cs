namespace Yathzee.UI.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seeds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileViewModels", "AverageScore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileViewModels", "AverageScore");
        }
    }
}
