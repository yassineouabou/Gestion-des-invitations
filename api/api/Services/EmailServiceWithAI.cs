using System.Net.Mail;
using System.Net;
using System.Text;
using api.Services.Interfaces;
using Newtonsoft.Json.Linq;
using api.Models;

namespace api.Services
{
    public class EmailServiceWithAI:IEmailServiceWithAI
    {
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly string _aiApiUrl;
        private readonly string _aiApiKey;

        public EmailServiceWithAI(IConfiguration configuration)
        {
            _fromEmail = configuration["FromEmail"]!;
            _fromPassword = configuration["FromPassword"]!;
            _aiApiUrl = configuration["AI_ApiUrl"]!;
            _aiApiKey = configuration["AI_ApiKey"]!;
        }

        public async Task<bool> EnvoyerEmailAvecAI(Visiteur visiteur,Evenement evenement)
        {
           
            string _fromName = evenement.Organisateur.Nom;
            string toEmail = visiteur.Email;
            string toName = visiteur.Nom;

            string prompt = $"Rédige un message d’invitation à l’événement '{evenement.Titre}' " +
                $"le {evenement.DateEvenement:dd MMM yyyy} à {evenement.Lieu}, " +
                $"pour le visiteur {visiteur.Nom}" +
                $"pour l'organisateur {evenement.Organisateur.Nom}";
            // 1. Appel à l’API DeepSeek
            var aiContent = await GenererContenuEmail(prompt);

            if (string.IsNullOrWhiteSpace(aiContent))
                return false;

            // 2. Préparer l’e-mail
            var fromAddress = new MailAddress(_fromEmail, _fromName);
            var toAddress = new MailAddress(toEmail, toName);
            string subject = $"Invitation à l’événement {evenement.Titre}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _fromPassword)
            };

            try
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = aiContent
                })
                {
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur d’envoi : " + ex.Message);
                return false;
            }
        }
          
        

        public async Task<string> GenererContenuEmail(string userPrompt)
        {
            var json = $@"
        {{
            ""messages"": [
                {{
                    ""role"": ""user"",
                    ""content"": ""{userPrompt}""
                }}
            ],
            ""model"": ""deepseek-ai/DeepSeek-V3-0324"",
            ""max_tokens"": 512,
            ""temperature"": 0.1,
            ""top_p"": 0.9
        }}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_aiApiKey}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(_aiApiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var jsonParse = JObject.Parse(responseBody);
                var contentAI = jsonParse["choices"]?[0]?["message"]?["content"]?.ToString();

                return contentAI ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur API DeepSeek : " + ex.Message);
                return "";
            }
        }
    }
}
