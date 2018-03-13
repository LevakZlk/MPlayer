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
    internal class SingerManager : ISingerManager
    {
        public ApplicationContext Database { get; set; }
        protected DbSet<Singer> DbSet { get; set; }
        public SingerManager(ApplicationContext db)
        {
            Database = db;
            DbSet = Database.Set<Singer>();
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public void Create(Singer item)
        {
            DbSet.Add(item);
        }

        public void Update(Singer item)
        {
            DbSet.AddOrUpdate(item);
        }

        public IEnumerable<Singer> GetAll()
        {
            return DbSet;
        }

        public Singer Get(string id)
        {
            return DbSet.Find(id);
        }

        public void Delete(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
    }
}
