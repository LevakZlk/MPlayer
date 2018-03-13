using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPlayer.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
        IGenreService CreateGenreService(string connection);
        ISingerService CreateSingerService(string connection);
        IMusicService CreateMusicService(string connection);
        IPlaylistService CreatePlaylistService(string connection);
    }
}
