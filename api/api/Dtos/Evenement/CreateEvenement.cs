﻿namespace api.Dtos.Evenement
{
    public class CreateEvenement
    {
        public string Titre { get; set; } = string.Empty;
        public string Categorie { get; set; } = string.Empty;
        public DateTime DateEvenement { get; set; }
        public string Lieu { get; set; } = string.Empty;
        
    }
}
