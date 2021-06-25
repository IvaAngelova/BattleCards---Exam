using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleCards.Data.Models
{
    public class Card
    {
        [Key]
        [Required]
        public string Id { get; set; }
            = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; } 
        
        [Required]
        public int Attack { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<UserCard> UserTrips { get; set; }
            = new List<UserCard>();
    }
}
