﻿namespace Shop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContactDetail1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactDetails", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactDetails", "Status", c => c.Boolean(nullable: false));
        }
    }
}
