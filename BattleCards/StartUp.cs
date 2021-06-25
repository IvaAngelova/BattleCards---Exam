using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BattleCards.Data;
using BattleCards.Services;

using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;

namespace BattleCards
{
    public class StartUp
    {
        public static async Task Main()
             => await HttpServer.WithRoutes(routes => routes
                 .MapStaticFiles()
                 .MapControllers())
             .WithServices(services => services
                 .Add<IViewEngine, CompilationViewEngine>()
                 .Add<IValidator, Validator>()
                 .Add<IPasswordHasher, PasswordHasher>()
                 .Add<BattleCardsDbContext>())
                 .WithConfiguration<BattleCardsDbContext>(context => context.Database.Migrate())
             .Start();
    }
}
