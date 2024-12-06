namespace SOA_CA2_Cian_Nojus.DTOs
{
    // Platform data transfer object
    public class PlatformDTO
    {
        public int id { get; set; } // primary key
        public string name { get; set; }
        public string manufacturer { get; set; }

        public List<string>? games { get; set; } = null;  // list of games that are available on this platform
    }
}
