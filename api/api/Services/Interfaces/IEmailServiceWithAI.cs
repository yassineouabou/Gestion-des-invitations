using api.Models;

namespace api.Services.Interfaces
{
    public interface IEmailServiceWithAI
    {
        Task<bool> EnvoyerEmailAvecAI(Verification verification);
        Task<string> GenererContenuEmail(string userPrompt);
    }
}
