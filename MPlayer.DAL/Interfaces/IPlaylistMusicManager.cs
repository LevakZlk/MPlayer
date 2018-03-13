using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.Interfaces
{
    public interface IPlaylistMusicManager
    {
        void Create(PlaylistMusic item);
        void Update(PlaylistMusic item);
        IEnumerable<PlaylistMusic> GetAll();
        IEnumerable<PlaylistMusic> GetAllByPlaylistId(string id);
        PlaylistMusic Get(string id);
        void Delete(string id);
        void Dispose();
        void DeleteByPlaylistId(string id);
    }
}
