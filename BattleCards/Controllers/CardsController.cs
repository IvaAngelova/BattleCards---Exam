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

        public HttpResponse Collection()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            var userId = this.User.Id;

            var collection = this.contex
                .UserCards
                .Where(u => u.UserId == userId)
                .Select(c => new CardListingViewModel
                {
                    Id = c.Card.Id,
                    Name = c.Card.Name,
                    Image = c.Card.ImageUrl,
                    Keyword = c.Card.Keyword,
                    Attack = c.Card.Attack,
                    Health = c.Card.Health,
                    Description = c.Card.Description
                })
                .ToList();

            return View(collection);
        }

        public HttpResponse AddToCollection(string cardId)
        {
            var userId = this.User.Id;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(cardId))
            {
                return Error("UserId and cardId can not be null or empty");
            }

            var checkForUserCard = this.contex
                .UserCards
                .Any(c => c.CardId == cardId && c.UserId == userId);

            if (checkForUserCard)
            {
                return Error("Card is already in the collection.");
            }

            var newUserCard = new UserCard
            {
                CardId = cardId,
                UserId = userId
            };

            this.contex.UserCards.Add(newUserCard);
            this.contex.SaveChanges();

            return Redirect("/Cards/Collection");
        }

        public HttpResponse RemoveFromCollection(string cardId)
        {
            var userId = this.User.Id;

            var currentUserCard = this.contex
                .UserCards
                .FirstOrDefault(c => c.CardId == cardId && c.UserId == userId);

            this.contex.Remove(currentUserCard);
            this.contex.SaveChanges();

            return this.Redirect("/Cards/Collection");
        }
    }
}
