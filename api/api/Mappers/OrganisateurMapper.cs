using api.Dtos.Organisateur;
using api.Models;

namespace api.Mappers
{
    public static class OrganisateurMapper
    {
        public static OrganisateurDto fromOrganisateur(this Organisateur organisateur)
        {
            return new OrganisateurDto
            {
                Id = organisateur.Id,
                Nom = organisateur.Nom,
                Email = organisateur.Email,
                Password = organisateur.Password
            };
        }

        public static Organisateur fromCreatOrganisateur(this CreateOrganisateur createOrganisateur)
        {
            return new Organisateur
            {
                Nom = createOrganisateur.Nom,
                Email = createOrganisateur.Email,
                Password = createOrganisateur.Password
            };
        }

    }
}
