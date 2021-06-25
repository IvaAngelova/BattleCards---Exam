using MyWebServer.Controllers;

using BattleCards.Data;
using BattleCards.Services;
using MyWebServer.Http;
using System.Linq;
using BattleCards.Models.Cards;
using BattleCards.Data.Models;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly IValidator validator;
        private readonly BattleCardsDbContext contex;

        public CardsController(IValidator validator, BattleCardsDbContext contex)
        {
            this.validator = validator;
            this.contex = contex;
        }

        public HttpResponse All()
        {
            var cards = this.contex
                .Cards
                .Select(t => new CardListingViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Image = t.ImageUrl,
                    Keyword = t.Keyword,
                    Attack = t.Attack,
                    Health = t.Health,
                    Description = t.Description
                })
                .ToList();

            return View(cards);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.User.IsAuthenticated)
            {
                return View();
            }

            return Error("Only users can create repositories.");
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCardViewModel model)
        {
            var modelErrors = this.validator.ValidateCardCreation(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var card = new Card
            {
                Name = model.Name,
                ImageUrl = model.Image,
                Keyword = model.Keyword,
                Attack = model.Attack,
                Health = model.Health,
                Description = model.Description
            };

            contex.Cards.Add(card);

            contex.SaveChanges();

            return Redirect("/Cards/All");
        }
    }
}
