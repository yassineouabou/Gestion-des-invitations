namespace api.Dtos.Evenement
{
    public class EvenementDto
    {
        public long Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public DateTime DateEvenement { get; set; }
        public string Lieu { get; set; } = string.Empty;
        public string Lien { get; set; } = string.Empty;
        public int Participantes { get; set; }
        public string Categorie { get; set; } = string.Empty;
        public long? OrganisateurId { get; set; }
    }
}
