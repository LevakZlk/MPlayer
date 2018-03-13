using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.BLL.Interfaces;
using MPlayer.DAL.Repositories;

namespace MPlayer.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }

        public IGenreService CreateGenreService(string connection)
        {
            return new GenreService(new IdentityUnitOfWork(connection));
        }

        public ISingerService CreateSingerService(string connection)
        {
            return new SingerService(new IdentityUnitOfWork(connection));
        }
        public IMusicService CreateMusicService(string connection)
        {
            return new MusicService(new IdentityUnitOfWork(connection));
        }
        public IPlaylistService CreatePlaylistService(string connection)
        {
            return new PlaylistService(new IdentityUnitOfWork(connection));
        }
    }
}
