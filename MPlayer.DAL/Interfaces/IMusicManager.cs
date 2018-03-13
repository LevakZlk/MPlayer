using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.Interfaces
{
    public interface IMusicManager
    {
        void Create(Music item);
        void Update(Music item);
        IEnumerable<Music> GetAll();
        IEnumerable<Music> GetAllById(string id);
        Music Get(string id);
        void Delete(string id);
        void Dispose();
    }
}
