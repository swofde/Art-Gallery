namespace bizzart_proj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class simonchmo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "ImagePath");
        }
    }
}
