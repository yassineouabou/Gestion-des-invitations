namespace api.Dtos.Evenement
{
    public class CreateEvenement
    {
        public string Titre { get; set; } = string.Empty;
        public DateTime DateEvenement { get; set; }
        public string Lieu { get; set; } = string.Empty;
        public string Lien { get; set; } = string.Empty;
        
    }
}
