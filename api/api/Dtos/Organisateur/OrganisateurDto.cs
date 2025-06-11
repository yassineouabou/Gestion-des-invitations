namespace api.Dtos.Organisateur
{
    public class OrganisateurDto
    {
        public long Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
