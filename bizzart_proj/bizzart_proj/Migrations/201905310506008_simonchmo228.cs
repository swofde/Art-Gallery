namespace bizzart_proj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class simonchmo228 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pictures", "ImagePath", c => c.String());
            DropColumn("dbo.Artists", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ImagePath", c => c.String());
            DropColumn("dbo.Pictures", "ImagePath");
        }
    }
}
