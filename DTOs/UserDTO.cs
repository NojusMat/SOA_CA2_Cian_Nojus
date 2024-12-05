namespace SOA_CA2_Cian_Nojus.DTOs
{
    public class UserDTO
    {
        public int id { get; set; } // primary key

        public string email { get; set; }

        public bool isAdministrator { get; set; }
    }
}
