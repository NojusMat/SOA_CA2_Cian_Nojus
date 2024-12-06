namespace SOA_CA2_Cian_Nojus.DTOs
{
    // Game data transfar object
    public class GameDTO
    {
        public int id { get; set; } // primary key
        public string title { get; set; }
        public string genre { get; set; }
        public int release_year { get; set; }
        public int developer_id { get; set; }

        public List<string>? platforms { get; set; } = null; // list of platforms that the game is available on
    }
}
