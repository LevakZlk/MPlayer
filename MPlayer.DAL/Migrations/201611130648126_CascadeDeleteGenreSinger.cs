namespace MPlayer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteGenreSinger : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Music", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Music", "SingerId", "dbo.Singer");
            DropIndex("dbo.Music", new[] { "GenreId" });
            DropIndex("dbo.Music", new[] { "SingerId" });
            AlterColumn("dbo.Music", "GenreId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Music", "SingerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Music", "GenreId");
            CreateIndex("dbo.Music", "SingerId");
            AddForeignKey("dbo.Music", "GenreId", "dbo.Genre", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Music", "SingerId", "dbo.Singer", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Music", "SingerId", "dbo.Singer");
            DropForeignKey("dbo.Music", "GenreId", "dbo.Genre");
            DropIndex("dbo.Music", new[] { "SingerId" });
            DropIndex("dbo.Music", new[] { "GenreId" });
            AlterColumn("dbo.Music", "SingerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Music", "GenreId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Music", "SingerId");
            CreateIndex("dbo.Music", "GenreId");
            AddForeignKey("dbo.Music", "SingerId", "dbo.Singer", "Id");
            AddForeignKey("dbo.Music", "GenreId", "dbo.Genre", "Id");
        }
    }
}
