using api.Dtos.Evenement;

namespace api.Dtos.Visiteur
{
    public class VisiteurAvecEvenementsDto
    {
        public long Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<EvenementDto> Evenements { get; set; } = new();
    }
}
