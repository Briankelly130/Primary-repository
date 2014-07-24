using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IGameRepository repository;

        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }

        public ViewResult Edit(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(p => p.GameID == gameId);
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGame(game);
                TempData["message"] = string.Format("{0} has been saved", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // There is something wrong with the data values
                return View(game);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Game());
        }

    }
}
