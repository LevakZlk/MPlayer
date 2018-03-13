using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;
using MPlayer.BLL.Interfaces;
using MPlayer.Models;

namespace MPlayer.Controllers
{
    public class SingerController : Controller
    {
        private ISingerService SingerService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ISingerService>();
            }
        }

        public ActionResult SingerView()
        {
            var singers = SingerService.GetAll().Select(singer => new SingerModel() { Id = singer.Id, Name = singer.Name }).ToList();
            return View(singers);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(SingerModel singer)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await SingerService.Create(new SingerDTO() { Name = singer.Name });
                if (operationDetails.Succedeed)
                    return RedirectToAction("SingerView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(singer);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Edit(string id)
        {
            var Singer = SingerService.GetById(id);
            SingerModel gm = new SingerModel() { Id = Singer.Id, Name = Singer.Name };
            return View(gm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(SingerModel Singer)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await SingerService.Update(new SingerDTO() { Id = Singer.Id, Name = Singer.Name });
                if (operationDetails.Succedeed)
                    return RedirectToAction("SingerView");
                else ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(Singer);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(string id)
        {
            SingerService.Delete(new SingerDTO() { Id = id });
            return RedirectToAction("SingerView");
        }
    }
}