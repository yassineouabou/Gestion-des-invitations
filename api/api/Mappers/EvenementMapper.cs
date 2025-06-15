using api.Dtos.Evenement;
using api.Models;

namespace api.Mappers
{
    public static class EvenementMapper
    {
        public static EvenementDto fromEvenement(this Evenement evenement)
        {
            return new EvenementDto
            {
                Id = evenement.Id,
                Titre = evenement.Titre,
                DateEvenement = evenement.DateEvenement,
                Lieu = evenement.Lieu,
                Lien = evenement.Lien,
                Participantes = evenement.Participantes,
                Categorie = evenement.Categorie,
                OrganisateurId = evenement.OrganisateurId
            };
        }

        public static Evenement toEvenementFromCreate(this CreateEvenement createEvenement,long organisateurId)
        {
            return new Evenement
            {
                Titre = createEvenement.Titre,
                DateEvenement = createEvenement.DateEvenement,
                Lieu = createEvenement.Lieu,
                Categorie = createEvenement.Categorie,
                OrganisateurId = organisateurId
            };
        }
    }
}
