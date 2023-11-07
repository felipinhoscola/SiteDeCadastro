using Microsoft.AspNetCore.Http;
using SiteDeCadastro.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SiteDeCadastro.Helper
{
    public class SessionUser : ISessionUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessionUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void CreateSessionUser(UserModel usuario)
        {
            string val = JsonSerializer.Serialize(usuario);

            _contextAccessor.HttpContext.Session.SetString("sessionUserLogin", val);
        }

        public UserModel GetSessionUser()
        {
            string sessionUser = _contextAccessor.HttpContext.Session.GetString("sessionUserLogin");

            if (string.IsNullOrEmpty(sessionUser)) return null;
            
            return JsonSerializer.Deserialize<UserModel>(sessionUser);

        }

        public void RemoveSessionUser()
        {
            _contextAccessor.HttpContext.Session.Remove("sessionUserLogin");
        }
    }
}
