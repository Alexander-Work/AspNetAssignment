using System.ComponentModel.DataAnnotations;

namespace GameConnect.Models
{
    public class GamePlayer
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SocialMedia { get; set; }

        //parent relationship
        public int GameId { get; set; }
        public Game? Game { get; set; } = default!;



    }
}
