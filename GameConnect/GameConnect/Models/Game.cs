using System.ComponentModel.DataAnnotations;

namespace GameConnect.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        //[Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        // child
        public List<GamePlayer>? GamePlayer { get; set;}


    }
}
