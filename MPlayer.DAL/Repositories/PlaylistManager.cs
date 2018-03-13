using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.EF;
using MPlayer.DAL.Entities;
using MPlayer.DAL.Interfaces;

namespace MPlayer.DAL.Repositories
{
    internal class PlaylistManager:IPlaylistManager
    {
        public ApplicationContext Database { get; set; }
        protected DbSet<Playlist> DbSet { get; set; }
        public PlaylistManager(ApplicationContext db)
        {
            Database = db;
            DbSet = Database.Set<Playlist>();
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public void Create(Playlist item)
        {
            DbSet.Add(item);
        }

        public void Update(Playlist item)
        {
            DbSet.AddOrUpdate(item);
        }

        public IEnumerable<Playlist> GetAll()
        {

            return DbSet.Include(x => x.PlaylistMusic);
        }

        public IEnumerable<Playlist> GetAllById(string id)
        {
            return DbSet.Where(x => x.UserId == id).Include(x => x.PlaylistMusic).Include(x=>x.PlaylistMusic).AsEnumerable();
        }

        public Playlist Get(string id)
        {
            return DbSet.Find(id);
        }

        public void Delete(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
    }
}
