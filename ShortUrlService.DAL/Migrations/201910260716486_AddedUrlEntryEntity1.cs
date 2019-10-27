namespace ShortUrlService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUrlEntryEntity1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UrlEntries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        FullUrl = c.String(),
                        ShortUrl = c.String(),
                        RedirectionCount = c.Long(nullable: false),
                        UrlHashCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UrlEntries");
        }
    }
}
