using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.EF;
using MPlayer.DAL.Entities;
using MPlayer.DAL.Interfaces;

namespace MPlayer.DAL.Repositories
{
    internal class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        protected DbSet<ClientProfile> DbSet { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
            DbSet= Database.Set<ClientProfile>();
        }

        public void Create(ClientProfile item)
        {
            DbSet.Add(item);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
