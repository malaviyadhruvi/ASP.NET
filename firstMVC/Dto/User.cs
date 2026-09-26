namespace firstMVC.Dto
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
