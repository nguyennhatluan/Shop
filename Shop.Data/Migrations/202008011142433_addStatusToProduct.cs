namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStatusToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Products", "Status");
        }
    }
}
