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
    public class PlaylistMusicManager: IPlaylistMusicManager
    {
        public ApplicationContext Database { get; set; }
        protected DbSet<PlaylistMusic> DbSet { get; set; }
        public PlaylistMusicManager(ApplicationContext db)
        {
            Database = db;
            DbSet = Database.Set<PlaylistMusic>();
        }
        public void Create(PlaylistMusic item)
        {
            DbSet.Add(item);
        }

        public void Update(PlaylistMusic item)
        {
            DbSet.AddOrUpdate(item);
        }

        public IEnumerable<PlaylistMusic> GetAll()
        {
            return DbSet;
        }

    

        public IEnumerable<PlaylistMusic> GetAllByPlaylistId(string id)
        {
           var d=DbSet.Where(x => x.PlaylistId == id).Include(x=>x.Music).Include(x=>x.Playlist).AsEnumerable();
            return d;
        }

        public PlaylistMusic Get(string id)
        {
            return DbSet.Find(id);
        }

        public void Delete(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void DeleteByPlaylistId(string playlistId)
        {
            IEnumerable<PlaylistMusic> range = DbSet.Where(x => x.PlaylistId == playlistId).ToList();
            DbSet.RemoveRange(range);
        }
    }
}
