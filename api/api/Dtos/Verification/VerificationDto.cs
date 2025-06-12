using api.Dtos.Evenement;
using api.Dtos.Visiteur;
using api.Enums;

namespace api.Dtos.Verification
{
    public class VerificationDto
    {
        public long Id { get; set; }
        public StatutVerification Etat { get; set; }
        public long? VisiteurId { get; set; }
        public long? EvenementId { get; set; }
        public VisiteurDto? VisiteurDto { get; set; }
        public EvenementDto? EvenementDto { get; set; }
    }
}
