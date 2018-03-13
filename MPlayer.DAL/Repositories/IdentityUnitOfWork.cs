using MPlayer.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using MPlayer.DAL.EF;
using MPlayer.DAL.Entities;
using MPlayer.DAL.Identity;

namespace MPlayer.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private IGenreManager genreManager;
        private ISingerManager singerManager;
        private IMusicManager musicManager;
        private IPlaylistManager playlistManager;
        private IPlaylistMusicManager playlistMusicManager;
        public IdentityUnitOfWork(string connectionString)
        {
            

            db = new ApplicationContext();
            userManager = new ApplicationUserManager(new UserStore<User>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<Role>(db));
            clientManager = new ClientManager(db);
            genreManager = new GenreManager(db);
            singerManager=new SingerManager(db);
            musicManager=new MusicManager(db);
            playlistManager=new PlaylistManager(db);
            playlistMusicManager=new PlaylistMusicManager(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IGenreManager GenreManager
        { get { return genreManager; } }

        public ISingerManager SingerManager {
            get { return singerManager; }
        }

        public IMusicManager MusicManager
        {
            get { return musicManager; }
        }

        public IPlaylistManager PlaylistManager
        {
            get { return playlistManager; }
        }

        public IPlaylistMusicManager PlaylistMusicManager
        {
            get { return playlistMusicManager; }
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        internal virtual void Dispose(bool disposing)
        {
            if (this.disposed) return;
            if (disposing)
            {
                db.Dispose();
                userManager.Dispose();
                roleManager.Dispose();
                clientManager.Dispose();
                genreManager.Dispose();
                singerManager.Dispose();
                musicManager.Dispose();
                playlistManager.Dispose();
                playlistMusicManager.Dispose();
            }
            this.disposed = true;
        }
    }
}
