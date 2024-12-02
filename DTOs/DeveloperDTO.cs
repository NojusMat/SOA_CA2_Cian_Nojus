namespace SOA_CA2_Cian_Nojus.DTOs
{
    public class DeveloperDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }

        public List<string>? games { get; set; } = null;
    }
}
