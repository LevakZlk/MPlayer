namespace MPlayer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Music",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 255),
                        Path = c.String(nullable: false, maxLength: 255),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Music", "UserId", "dbo.User");
            DropIndex("dbo.Music", new[] { "UserId" });
            DropTable("dbo.Music");
        }
    }
}
