﻿using api.Dtos.Visiteur;
using api.Models;

namespace api.Mappers
{
    public static class VisiteurMapper
    {
        public static VisiteurDto fromVisiteur(this Visiteur visiteur)
        {
            return new VisiteurDto
            {
                Id = visiteur.Id,
                Nom = visiteur.Nom,
                Email = visiteur.Email,
                Phone = visiteur.Phone
            };
        }

        public static Visiteur fromCreateVisiteur(this CreateVisiteur createVisiteur)
        {
            return new Visiteur
            {
                Nom = createVisiteur.Nom,
                Email = createVisiteur.Email,
                Phone = createVisiteur.Phone
            };
        }

        public static VisiteurAvecEvenementsDto ToVisiteurAvecEvenementsDto(this Visiteur visiteur,long organisateurId)
        {
            return new VisiteurAvecEvenementsDto
            {
                Id = visiteur.Id,
                Nom = visiteur.Nom,
                Email = visiteur.Email,
                Phone = visiteur.Phone,
                Evenements = visiteur.Verifications
                    .Where(v => v.Evenement != null)
                    .Select(v => v.Evenement!.fromEvenement())
                    .DistinctBy(e => e.Id) 
                    .ToList()
            };
        }
    }
}
