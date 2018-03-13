using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.Interfaces
{
    public interface IPlaylistManager : IDisposable
    {
        void Create(Playlist item);
        void Update(Playlist item);
        IEnumerable<Playlist> GetAll();
        IEnumerable<Playlist> GetAllById(string id);
        Playlist Get(string id);
        void Delete(string id);
    }
}
