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
    internal class GenreManager:IGenreManager
    {
        public ApplicationContext Database { get; set; }
        protected DbSet<Genre> DbSet { get; set; }
        public GenreManager(ApplicationContext db)
        {
            Database = db;
            DbSet = Database.Set<Genre>();
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public void Create(Genre item)
        {
            DbSet.Add(item);
        }

        public void Update(Genre item)

        {
            DbSet.AddOrUpdate(item);
        }

        public IEnumerable<Genre> GetAll()
        {
            return DbSet;
        }

        public Genre Get(string id)
        {
            return DbSet.Find(id);
        }

        public void Delete(string id)
        {
            DbSet.Remove(DbSet.Find(id));
        }
    }
}
