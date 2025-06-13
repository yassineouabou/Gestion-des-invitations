using System.Net.Mail;
using System.Net;
using System.Text;
using api.Services.Interfaces;
using Newtonsoft.Json.Linq;
using api.Models;
using System.Text.Json;

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

        public async Task<bool> EnvoyerEmailAvecAI(Verification verification)
        {
           
            string _fromName = verification.Evenement.Organisateur.Nom;
            string toEmail = verification.Visiteur.Email;
            string toName = verification.Visiteur.Nom;

            string prompt = $"Génère un message de confirmation de participation à un événement en français, avec les éléments suivants :  " +
    $"\r\n- Destinataire : {toName} " +
    $"\r\n- Organisateur : {_fromName}  " +
    $"\r\n- Nom de l'événement : {verification.Evenement.Titre}  " +
    $"\r\n- Date : {verification.Evenement.DateEvenement}  " +
    $"\r\n- Lieu : {verification.Evenement.Lieu}  " +
    $"\r\n- Liens d'action :  " +
    $"\r\n  - Lien \"Accepter\" : http://localhost....  " +
    $"\r\n  - Lien \"Refuser\" : http://localhost:SSQSQS " +
    $"\r\n\r\nConsignes : " +
    $"\r\n1. Format : Corps de message uniquement (pas d'objet, pas de signature).  " +
    $"\r\n2. Style : Courtois, neutre et professionnel.  " +
    $"\r\n3. Éviter tout placeholder (comme \"[Votre nom]\") ou champ incomplet.  " +
    $"\r\n4. Structurer clairement les informations (date, lieu, liens cliquables).  " +
    $"\r\n5. Ne pas utiliser de mise en forme Markdown (pas de * ou **). " +
    $"\r\n6. Inclure une phrase de remerciement.";



            // 1. Appel à l’API DeepSeek
            var aiContent = await GenererContenuEmail(prompt);

            if (string.IsNullOrWhiteSpace(aiContent))
                return false;

            // 2. Préparer l’e-mail
            var fromAddress = new MailAddress(_fromEmail, _fromName);
            var toAddress = new MailAddress(toEmail, toName);
            string subject = $"Invitation à l’événement {verification.Evenement.Titre}";

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
           var payload = new
            {
                messages = new[]
                {
                    new { role = "user", content = userPrompt }
                },
                model = "deepseek-ai/DeepSeek-V3-0324",
                max_tokens = 512,
                temperature = 0.1,
                top_p = 0.9
            };

            string json = JsonSerializer.Serialize(payload);


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_aiApiKey}");
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(_aiApiUrl, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Réponse brute : " + responseBody);


                var jsonParse = JObject.Parse(responseBody);


                var choices = jsonParse["choices"];
                if (choices == null || !choices.Any())
                {
                    Console.WriteLine("Aucune réponse dans 'choices'");
                    return "";
                }

                var contentAI = jsonParse["choices"]?[0]?["message"]?["content"]?.ToString();

                Console.WriteLine("IA Content: " + contentAI);

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
