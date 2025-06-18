import { EvenementDto } from "./Evenement.model";
import { VisiteurDto } from "./Visiteur.model";

export interface InvitationDto {
    id:           number;
    etat:         string;
    visiteurId:   number;
    evenementId:  number;
    visiteurDto:  VisiteurDto;
    evenementDto: EvenementDto;
}
