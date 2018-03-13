using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;
using MPlayer.BLL.Interfaces;
using MPlayer.Models;

namespace MPlayer.Controllers
{
    public class PlaylistController : Controller
    {
        private IMusicService MusicService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IMusicService>();
            }
        }
        private IPlaylistService PlaylistService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IPlaylistService>();
            }
        }
        private ISingerService SingerService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ISingerService>();
            }
        }
        private IGenreService GenreService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IGenreService>();
            }
        }
        public ActionResult PlaylistView()
        {
            var t = User.Identity.GetUserId();
            List<PlaylistModel> playlists=new List<PlaylistModel>();
            if (User.IsInRole("admin"))
            {
                playlists = PlaylistService.GetAll().Select(playlist => new PlaylistModel()
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    Musics = playlist.Musics.Select(x => new MusicModel() {Id = x.Id}).ToList()
                }).ToList();
            }
            else
            {
                playlists = PlaylistService.GetAllById(t).Select(playlist => new PlaylistModel()
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    Musics = playlist.Musics.Select(x => new MusicModel() { Id = x.Id }).ToList()
                }).ToList();
            }
            return View(playlists);
        }

        public ActionResult Create()
        {
            var list=MusicService.GetAll().Select(m=>new {Id=m.Id,Name=m.Name}).ToList();
            PlaylistChangeModel.GetMusicSelectItems=new MultiSelectList(list,"Id","Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlaylistChangeModel pmodel)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await PlaylistService.Create(new PlaylistDTO()
                {
                    Name = pmodel.Name,
                    UserId = User.Identity.GetUserId(),
                    Musics = pmodel.MusicsId.Select(x=>new MusicDTO() {Id=x}).ToList()
                });
                if (operationDetails.Succedeed)
                    return RedirectToAction("PlaylistView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(pmodel);
        }

        public ActionResult Edit(string id)
        {
            var list = MusicService.GetAll().Select(m => new { Id = m.Id, Name = m.Name }).ToList();
          
            var playlist = PlaylistService.GetById(id);
            PlaylistChangeModel.GetMusicSelectItems = new MultiSelectList(list, "Id", "Name",playlist.Musics.Select(x=>x.Id).ToList());
            PlaylistChangeModel gm = new PlaylistChangeModel()
            {
                Id = playlist.Id,
                Name = playlist.Name
            };
            return View(gm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlaylistChangeModel pmodel)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await PlaylistService.Update(new PlaylistDTO()
                {
                    Id=pmodel.Id,
                    Name = pmodel.Name,
                    UserId = User.Identity.GetUserId(),
                    Musics = pmodel.MusicsId.Select(x => new MusicDTO() { Id = x }).ToList()
                });
                if (operationDetails.Succedeed)
                    return RedirectToAction("PlaylistView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(pmodel);
        }

        public ActionResult ShowMusics(string id)
        {
            return RedirectToAction("MusicViewInPlaylist","Music",new { playlistId=id});
        }

        public ActionResult Delete(string id)
        {
            PlaylistService.Delete(new PlaylistDTO() { Id = id });
            return RedirectToAction("PlaylistView");
        }
    }
}