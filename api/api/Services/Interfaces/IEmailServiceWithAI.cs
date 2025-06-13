using api.Models;

namespace api.Services.Interfaces
{
    public interface IEmailServiceWithAI
    {
        Task<bool> EnvoyerEmailAvecAI(Visiteur visiteur, Evenement evenement);
        Task<string> GenererContenuEmail(string userPrompt);
    }
}
