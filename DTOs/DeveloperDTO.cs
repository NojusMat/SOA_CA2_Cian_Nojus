namespace SOA_CA2_Cian_Nojus.DTOs
{
    // Developer data transfer object
    public class DeveloperDTO
    {
        public int id { get; set; }  // primary key
        public string name { get; set; }
        public string country { get; set; }

        public List<String>? games { get; set; } = null; // list of games that the developer has made
    }
}
