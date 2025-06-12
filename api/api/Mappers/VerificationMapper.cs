using api.Dtos.Verification;
using api.Models;

namespace api.Mappers
{
    public static class VerificationMapper
    {
        public static VerificationDto fromVerification(this Verification verification)
        {
            return new VerificationDto
            {
                Id = verification.Id,
                Etat = verification.Etat,
                VisiteurId = verification.VisiteurId,
                EvenementId = verification.EvenementId,
                VisiteurDto = verification.Visiteur?.fromVisiteur(),
                EvenementDto = verification.Evenement?.fromEvenement()
            };
        }

        public static Verification fromCreateVerification(this CreateVerification createVerification, long evenementId, long visiteurId)
        {
            return new Verification
            {
                Etat = createVerification.Etat,
                VisiteurId = visiteurId,
                EvenementId = evenementId
            };
        }
    }
}
