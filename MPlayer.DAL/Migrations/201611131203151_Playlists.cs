namespace MPlayer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playlists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlaylistMusic",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MusicId = c.String(nullable: false, maxLength: 128),
                        PlaylistId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playlist", t => t.PlaylistId, cascadeDelete: true)
                .ForeignKey("dbo.Music", t => t.MusicId, cascadeDelete: true)
                .Index(t => t.MusicId)
                .Index(t => t.PlaylistId);
            
            CreateTable(
                "dbo.Playlist",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 255),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaylistMusic", "MusicId", "dbo.Music");
            DropForeignKey("dbo.Playlist", "UserId", "dbo.User");
            DropForeignKey("dbo.PlaylistMusic", "PlaylistId", "dbo.Playlist");
            DropIndex("dbo.Playlist", new[] { "UserId" });
            DropIndex("dbo.PlaylistMusic", new[] { "PlaylistId" });
            DropIndex("dbo.PlaylistMusic", new[] { "MusicId" });
            DropTable("dbo.Playlist");
            DropTable("dbo.PlaylistMusic");
        }
    }
}
