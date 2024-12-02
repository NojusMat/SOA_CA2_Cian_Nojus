namespace SOA_CA2_Cian_Nojus.DTOs
{
    public class PlatformDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }

        public List<string>? games { get; set; } = null;
    }
}
