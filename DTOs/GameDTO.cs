namespace SOA_CA2_Cian_Nojus.DTOs
{
    public class GameDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public int release_year { get; set; }
        public int developer_id { get; set; }

        public List<string>? platforms { get; set; } = null;
    }
}
