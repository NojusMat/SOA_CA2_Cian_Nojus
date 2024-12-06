namespace SOA_CA2_Cian_Nojus.Models
{
    // GamePlatform model
    public class GamePlatform
    {
        public int GameId { get; set; } 
        public Games Game { get; set; } = null!; // Navigation Property

        public int PlatformId { get; set; }
        public Platform Platform { get; set; } = null!; // Navigation Property
    }
}
