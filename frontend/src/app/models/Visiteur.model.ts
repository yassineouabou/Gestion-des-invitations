import { EvenementDto } from "./Evenement.model";

export interface VisiteurDto {
    id?:         number;
    nom:        string;
    email:      string;
    phone:      string;
    evenements?: EvenementDto[];
}
