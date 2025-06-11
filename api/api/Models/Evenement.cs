namespace api.Models
{
    public class Evenement
    {
        public long Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public DateTime DateEvenement { get; set; }
        public string Lieu { get; set; } = string.Empty;
        public string Lien { get; set; } = string.Empty;
        public int Participantes { get; set; } 
        public long? OrganisateurId { get; set; }
        public Organisateur? Organisateur { get; set; }
        public ICollection<Verification> Verifications { get; set; } = new List<Verification>();
    }
}
