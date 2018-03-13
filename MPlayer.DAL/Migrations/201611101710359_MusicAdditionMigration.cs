namespace MPlayer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicAdditionMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Music", "GenreId", c => c.String(maxLength: 128));
            AddColumn("dbo.Music", "SingerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Music", "GenreId");
            CreateIndex("dbo.Music", "SingerId");
            AddForeignKey("dbo.Music", "GenreId", "dbo.Genre", "Id");
            AddForeignKey("dbo.Music", "SingerId", "dbo.Singer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Music", "SingerId", "dbo.Singer");
            DropForeignKey("dbo.Music", "GenreId", "dbo.Genre");
            DropIndex("dbo.Music", new[] { "SingerId" });
            DropIndex("dbo.Music", new[] { "GenreId" });
            DropColumn("dbo.Music", "SingerId");
            DropColumn("dbo.Music", "GenreId");
        }
    }
}
