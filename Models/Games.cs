namespace SOA_CA2_Cian_Nojus.Models
{
    // Games model
    public class Games
    {
        internal readonly object id;

        public int Id { get; set; } // Primary Key
        public string title { get; set; }
        public string genre { get; set; }
        public int release_year { get; set; }
        public int developer_id { get; set; } // Foreign Key

        public Developer Developer { get; set; } // Navigation Property

        public ICollection<GamePlatform> GamePlatforms { get; } = []; // Navigation Property
    }
}
