using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MPlayer.DAL.Entities;
using MPlayer.DAL.Migrations;
using MPlayer.DAL.TableConfigurations;

namespace MPlayer.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Configuration>());
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistMusic> PlaylistMusics { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new ClientProfileMap());

            modelBuilder.Configurations.Add(new GenreMap());
            modelBuilder.Configurations.Add(new SingerMap());
            modelBuilder.Configurations.Add(new MusicMap());
            modelBuilder.Configurations.Add(new PlaylistMap());
            modelBuilder.Configurations.Add(new PlaylistMusicMap());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
