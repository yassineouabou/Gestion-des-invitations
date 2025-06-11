using api.Dtos.Evenement;
using api.Models;

namespace api.Mappers
{
    public static class EvenementMapper
    {
        public static EvenementDto fromEvenement(Evenement evenement)
        {
            return new EvenementDto
            {
                Id = evenement.Id,
                Titre = evenement.Titre,
                DateEvenement = evenement.DateEvenement,
                Lieu = evenement.Lieu,
                Lien = evenement.Lien,
                Participantes = evenement.Participantes,
                OrganisateurId = evenement.OrganisateurId
            };
        }

        public static Evenement toEvenementFromCreate(CreateEvenement createEvenement,long organisateurId)
        {
            return new Evenement
            {
                Titre = createEvenement.Titre,
                DateEvenement = createEvenement.DateEvenement,
                Lieu = createEvenement.Lieu,
                Lien = createEvenement.Lien,
                Participantes = createEvenement.Participantes,
                OrganisateurId = organisateurId
            }
        }
    }
}
