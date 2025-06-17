namespace api.Models
{
    public class Visiteur
    {
        public long Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ICollection<Verification> Verifications { get; set; } = new List<Verification>();
    }
}
