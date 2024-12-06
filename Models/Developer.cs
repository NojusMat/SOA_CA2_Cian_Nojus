namespace SOA_CA2_Cian_Nojus.Models
{
    // Developer model
    public class Developer
    {
        public int Id { get; set; } // Primary Key
        public string name { get; set; }
        public string country { get; set; }


        public ICollection<Games> Games { get; set; } // Navigation Property
    }
}
