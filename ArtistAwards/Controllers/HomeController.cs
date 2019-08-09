using Microsoft.AspNetCore.Mvc;
using ArtistAwards.DAL.Interface;

namespace ArtistAwards.Controllers
{
    public class HomeController : Controller
    {
        private IAwardService awardService;

        public HomeController(IAwardService awardService)
        {
            this.awardService = awardService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetData()
        {
            return Json(new { data = awardService.GetAll() });
        }
    }
}
