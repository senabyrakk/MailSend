using MailSend.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailSend.Services
{
    public static class MailManager
    {
        private static IConfigurationRoot _config
        {
            get
            {
                return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            }
        }

        public static async Task<string> SendAsync(MailDto dto)
        {
            try
            {
                string smtpHost = _config.GetSection("MailingService").GetSection("SmtpHost")?.Value;
                string smtpPort = _config.GetSection("MailingService").GetSection("SmtpPort")?.Value;
                string fromAddress = _config.GetSection("MailingService").GetSection("FromAddress")?.Value;
                string password = _config.GetSection("MailingService").GetSection("Password")?.Value;
                string toAddress = _config.GetSection("MailingService").GetSection("ToAddress")?.Value;

                using (var client = new SmtpClient(smtpHost, int.Parse(smtpPort)))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(fromAddress, password);

                    MailMessage message = new MailMessage();

                    message.From = new System.Net.Mail.MailAddress(fromAddress);
                    message.To.Add(toAddress);
                    message.Body = dto.Body;
                    message.Subject = dto.Subject;
                    message.IsBodyHtml = true;

                    await Task.Run(() => client.Send(message));
                    return "Başarılı";
                }
            }
            catch (System.Exception ex)
            {

                return ex.Message;
            }
        }

    }
}
