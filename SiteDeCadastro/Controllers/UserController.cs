using Microsoft.AspNetCore.Mvc;

namespace SiteDeCadastro.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
