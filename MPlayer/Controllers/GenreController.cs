using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;
using MPlayer.BLL.Interfaces;
using MPlayer.BLL.Services;
using MPlayer.Models;

namespace MPlayer.Controllers
{
    public class GenreController : Controller
    {

        private IGenreService GenreService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IGenreService>();
            }
        }

        public ActionResult GenreView()
        {
            var genres = GenreService.GetAll().Select(genre => new GenreModel() { Id = genre.Id, Name = genre.Name }).ToList();
            return View(genres);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await GenreService.Create(new GenreDTO() {Name=genre.Name});
                if (operationDetails.Succedeed)
                    return RedirectToAction("GenreView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(genre);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            var genre = GenreService.GetById(id);
            GenreModel gm = new GenreModel() {Id = genre.Id, Name = genre.Name};
            return View(gm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await GenreService.Update(new GenreDTO() {Id = genre.Id,Name = genre.Name});
                if (operationDetails.Succedeed)
                    return RedirectToAction("GenreView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(genre);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id)
        {
            GenreService.Delete(new GenreDTO() {Id = id});
             return RedirectToAction("GenreView");
        }
    }
}