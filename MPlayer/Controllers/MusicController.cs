using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;
using MPlayer.BLL.Interfaces;
using MPlayer.BLL.Services;
using MPlayer.Models;

namespace MPlayer.Controllers
{
    public class MusicController : Controller
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

        public ActionResult MusicView()
        {
            
            var musics = MusicService.GetAll().Select(music => new MusicModel()
            {
              Id = music.Id,
              Name = music.Name,
              Path=music.Path,
              SingerName = SingerService.GetById(music.SingerId).Name,
              GenreName= GenreService.GetById(music.GenreId).Name
              
            }).ToList();
            return View(musics);
        }
     

        public ActionResult MusicViewSearch(string sText,string sType)
        {
            var musics = MusicService.GetAll().Select(music => new MusicModel()
            {
                Id = music.Id,
                Name = music.Name,
                Path = music.Path,
                SingerName = SingerService.GetById(music.SingerId).Name,
                GenreName = GenreService.GetById(music.GenreId).Name
            }).ToList();
            return View("MusicView",musics);
        }

        public ActionResult Create()
        {
            MusicChangeModel.GetSingerSelectItems =
                SingerService.GetAll().Select(m => new SelectListItem() {Text = m.Name, Value = m.Id});
            MusicChangeModel.GetGenreSelectItems =
                GenreService.GetAll().Select(m => new SelectListItem() { Text = m.Name, Value = m.Id });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MusicChangeModel music)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(music.File.FileName);
                var path = Path.Combine("/uploads/", fileName);
                OperationDetails operationDetails = await MusicService.Create(new MusicDTO()
                {
                    Name = music.Name,
                    Path = path,
                    GenreId = music.GenreId,
                    SingerId = music.SingerId,
                    UserId = User.Identity.GetUserId()
                });
                music.File.SaveAs(Server.MapPath(path));
                if (operationDetails.Succedeed)
                    return RedirectToAction("MusicView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(music);
        }


        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
           
                var music = MusicService.GetById(id);
                MusicChangeModel.GetSingerSelectItems =
                    SingerService.GetAll().Select(m => new SelectListItem() {Text = m.Name, Value = m.Id});
                MusicChangeModel.GetGenreSelectItems =
                    GenreService.GetAll().Select(m => new SelectListItem() {Text = m.Name, Value = m.Id});
                MusicChangeModel gm = new MusicChangeModel()
                {
                    Id = music.Id,
                    Name = music.Name,
                    SingerId = music.SingerId,
                    GenreId = music.GenreId
                };
            
            return View(gm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(MusicChangeModel music)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(music.File.FileName);
                var path = Path.Combine("/uploads/", fileName);
                OperationDetails operationDetails = await MusicService.Update(
                   new MusicDTO()
                   {
                       Id=music.Id,
                       Name = music.Name,
                       Path = path,
                       GenreId = music.GenreId,
                       SingerId = music.SingerId,
                       UserId = User.Identity.GetUserId()
                   }
                );
                music.File.SaveAs(Server.MapPath(path));
                if (operationDetails.Succedeed)
                    return RedirectToAction("MusicView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(music);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var platlists=PlaylistService.GetAll().Select(x => x.Musics).ToList();
            bool f = platlists.All(s => Equals(s.Select(x => x.Id).Count(x => x == id), 0));
            if (!f)
            {
                ModelState.AddModelError("DeleteMusic", "Music in playlist");
                return View("ErrorMusicDelete",new MusicModel() {DeleteMusic = "Music in playlist. Can't delete"});
            }
            OperationDetails operationDetails = await MusicService.Delete(new MusicDTO() { Id = id });
            return RedirectToAction("MusicView");
        }


        public ActionResult MusicViewInPlaylist(string playlistId)
        {
            var t = User.Identity.GetUserId();
            var musics = PlaylistService.GetById(playlistId).Musics;
            List<MusicDTO> d = musics.Select(m => MusicService.GetById(m.Id)).ToList();
            var musicс = d.Select(music => new MusicModel()
            {
                Id = music.Id,
                Name = music.Name,
                Path = music.Path,
                SingerName = SingerService.GetById(music.SingerId).Name,
                GenreName = GenreService.GetById(music.GenreId).Name
            }).ToList();
            musicс.First().PlaylistName = PlaylistService.GetById(playlistId).Name;
            return View(musicс);
        }



        [HttpPost]
        public ActionResult Filter(FormCollection formCollection)
        {
            var searchfieldid = formCollection.Get("SearchType");
            var searchfield = formCollection.Get("SearchString");
            var musics = MusicService.GetAll().Select(music => new MusicModel()
            {
                Id = music.Id,
                Name = music.Name,
                Path = music.Path,
                SingerName = SingerService.GetById(music.SingerId).Name,
                GenreName = GenreService.GetById(music.GenreId).Name

            }).ToList();
            switch (searchfieldid)
            {
                case "0":
                    musics = musics.Where(x => x.Name.ToLower().Contains(searchfield.ToLower())).ToList();
                    break;
                case "1":
                    musics = musics.Where(x => x.SingerName.ToLower().Contains(searchfield.ToLower())).ToList();
                    break;
                case "2":
                    musics = musics.Where(x => x.GenreName.ToLower().Contains(searchfield.ToLower())).ToList();
                    break;
            }
            return View("MusicView", musics);

        }
    }
}