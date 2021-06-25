using MyWebServer.Http;
using MyWebServer.Controllers;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    { 
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Cards/All");
            }

            return this.View();
        }
    }
}