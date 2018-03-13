using System.IO;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using MPlayer.BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using MPlayer.BLL.Interfaces;


[assembly: OwinStartup(typeof(MPlayer.App_Start.Startup))]

namespace MPlayer.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public virtual void Configuration(IAppBuilder app)
        {
            
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.CreatePerOwinContext<IGenreService>(CreateGenreService);
            app.CreatePerOwinContext<ISingerService>(CreateSingerService);
            app.CreatePerOwinContext<IMusicService>(CreateMusicService);
            app.CreatePerOwinContext<IPlaylistService>(CreatePlaylistService);

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

      

        private ISingerService CreateSingerService()
        {
            return serviceCreator.CreateSingerService("DefaultConnection");
        }

        private IGenreService CreateGenreService()
        {
            return serviceCreator.CreateGenreService("DefaultConnection");
        }

        private IMusicService CreateMusicService()
        {
            return serviceCreator.CreateMusicService("DefaultConnection");
        }

        private IPlaylistService CreatePlaylistService()
        {
            return serviceCreator.CreatePlaylistService("DefaultConnection");
        }
        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }
    }
}