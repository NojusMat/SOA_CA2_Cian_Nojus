namespace SOA_CA2_Cian_Nojus.Models
{
    public class Games
    {
        public int game_id { get; set; } // Primary Key
        public string title { get; set; }
        public string genre { get; set; }
        public int release_year { get; set; }
        public int developer_id { get; set; } // Foreign Key

    }
}
