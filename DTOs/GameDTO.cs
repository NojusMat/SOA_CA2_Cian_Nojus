namespace SOA_CA2_Cian_Nojus.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public int release_year { get; set; }
        public int developerId { get; set; }

        public List<PlatformDTO> Platforms { get; set; }
    }
}
