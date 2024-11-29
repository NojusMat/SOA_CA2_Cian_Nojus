namespace SOA_CA2_Cian_Nojus.DTOs
{
    public class PlatformDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string manufacturer { get; set; }

        public List<string> Games { get; set; }
    }
}
