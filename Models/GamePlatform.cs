namespace SOA_CA2_Cian_Nojus.Models
{
    public class GamePlatform
    {
        public int GameId { get; set; }
        public Games Game { get; set; } = null!;

        public int PlatformId { get; set; }
        public Platform Platform { get; set; } = null!;
    }
}
