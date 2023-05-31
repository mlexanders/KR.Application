using Microsoft.AspNetCore.Mvc;
using MusalovKR.Repositories;

namespace MusalovKR.Controllers
{
    public class ExecutionController : Controller
    {
        private readonly PostsRepository repository;

        public ExecutionController(PostsRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index(string query)
        {
            if (query == null) return View();

            try
            {
                var result = repository.ReadWithScript(query);
                return View(result);
            }
            catch (Exception e)
            {
                ViewData.Add("response", e.Message);
                return View();
            }
        }
    }
}
