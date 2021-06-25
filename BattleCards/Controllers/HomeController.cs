using MyWebServer.Http;
using MyWebServer.Controllers;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    { 
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}