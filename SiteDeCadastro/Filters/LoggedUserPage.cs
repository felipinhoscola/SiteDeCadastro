using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiteDeCadastro.Models;
using System.Text.Json;

namespace SiteDeCadastro.Filters
{
    public class LoggedUserPage : ActionFilterAttribute //Herdou de uma classe ja criada dentro do ASP.NEt
    {
        //override sobrescreve um metodo da classe pai
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessionUser = context.HttpContext.Session.GetString("sessionUserLogin");

            if (string.IsNullOrEmpty(sessionUser) ) 
            {
                // RedirectToRouteResult retorna para a rota que esta dentro do (), RouteValueDictionary chama um valor de rota
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                UserModel user = JsonSerializer.Deserialize<UserModel>(sessionUser);
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);//base. mantem/continua o codigo do metodo original.
        }
    }
}
