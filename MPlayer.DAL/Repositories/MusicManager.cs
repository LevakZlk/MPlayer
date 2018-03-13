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
    public class MusicManager:IMusicManager
    {
        public ApplicationContext Database { get; set; }
        protected DbSet<Music> DbSet { get; set; }
        public MusicManager(ApplicationContext db)
        {
            Database = db;
            DbSet = Database.Set<Music>();
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public void Create(Music item)
        {
            DbSet.Add(item);
        }

        public void Update(Music item)
        {
            DbSet.AddOrUpdate(item);
        }

        public IEnumerable<Music> GetAll()
        {
            return DbSet.Include(x => x.Genre).Include(x => x.Singer).AsEnumerable();
        }

        public IEnumerable<Music> GetAllById(string id)
        {
            return DbSet.Where(x => x.UserId == id).Include(x=>x.Genre).Include(x=>x.Singer).AsEnumerable();
        }

        public Music Get(string id)
        {
            return DbSet.Find(id);
        }

        public void Delete(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
    }
}
