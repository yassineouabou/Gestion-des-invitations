export interface EvenementDto {
    id:             number;
    titre:          string;
    dateEvenement:  Date;
    lieu:           string;
    lien:           string;
    participantes:  number;
    categorie:      string;
    organisateurId: number;
}