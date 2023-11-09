using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiteDeCadastro.Models;
using System.Text.Json;

namespace SiteDeCadastro.Filters
{
    public class LoggedAdminPage : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessionUser = context.HttpContext.Session.GetString("sessionUserLogin");

            if (string.IsNullOrEmpty(sessionUser) ) 
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                UserModel user = JsonSerializer.Deserialize<UserModel>(sessionUser);
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if(user.Perfil != Enums.PerfilEnum.Admin) 
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrict" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);//base. mantem/continua o codigo do metodo original.
        }
    }
}
