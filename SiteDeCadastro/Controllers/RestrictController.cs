using Microsoft.AspNetCore.Mvc;
using SiteDeCadastro.Filters;

namespace SiteDeCadastro.Controllers
{
    [LoggedUserPage]
    public class RestrictController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
