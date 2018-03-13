using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Identity;

namespace MPlayer.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IGenreManager GenreManager { get; }
        ISingerManager SingerManager { get; }
        IMusicManager MusicManager { get; }
        IPlaylistManager PlaylistManager { get; }
        IPlaylistMusicManager PlaylistMusicManager { get; }
        Task SaveAsync();
    }
}
