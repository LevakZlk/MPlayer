using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.Interfaces
{
    public interface ISingerManager : IDisposable
    {
        void Create(Singer item);
        void Update(Singer item);
        IEnumerable<Singer> GetAll();
        Singer Get(string id);
        void Delete(string id);
    }
}
