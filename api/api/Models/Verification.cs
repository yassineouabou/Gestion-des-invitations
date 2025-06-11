using api.Enums;

namespace api.Models
{
    public class Verification
    {
        public long Id { get; set; }
        public StatutVerification Etat { get; set; }
        public long? VisiteurId { get; set; }
        public long? EvenementId { get; set; }

        public Visiteur? Visiteur { get; set; }
        public Evenement? Evenement { get; set; } 
    }

}
