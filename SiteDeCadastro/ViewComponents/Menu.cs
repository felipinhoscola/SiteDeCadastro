using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using SiteDeCadastro.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiteDeCadastro.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            string sessionUser = HttpContext.Session.GetString("sessionUserLogin");

            if (string.IsNullOrEmpty(sessionUser)) return null;

            UserModel user = JsonSerializer.Deserialize<UserModel>(sessionUser);

            return View(user);
            
        }
    }
}
