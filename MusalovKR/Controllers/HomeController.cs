using Microsoft.AspNetCore.Mvc;
using MusalovKR.Models;
using MusalovKR.Repositories;
using MusalovKR.ViewModel;
using System.Diagnostics;

namespace MusalovKR.Controllers
{
    public class HomeController : Controller
    {
        private readonly PostsRepository repository;

        public HomeController(PostsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index(int skip = 0)
        {

            var count = (await repository.Read(0, int.MaxValue)).Count();
            var item = await repository.Read(skip);

            var viewModel = new PageViewModel()
            {
                Post = item,
                Index = skip,
                CanShowNext = skip < count - 1,
                CanShowPrevious = skip > 0,
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}