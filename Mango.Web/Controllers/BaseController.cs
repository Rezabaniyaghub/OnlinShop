using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string _accessToken => HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult() ?? "";
        protected string UserId => User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "";
    }
}
