using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SyncSpace.Domain.Helpers;
using System.Net.Mail;
using System.Net;

namespace SyncSpace.Application.Services.EmailService;

public class EmailService(IOptions<EmailProvider> options) : IEmailSender
{
    private readonly string _smtpEmail = options.Value.smptemail;
    private readonly string _smtpPassword = options.Value.smptpassword;
    private readonly string _smtpHost = "smtp.gmail.com";
    private readonly int _smtpPort =  587;
    private readonly bool _enableSsl = true;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(_smtpEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(email);

        using var smtpClient = new SmtpClient(_smtpHost, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpEmail, _smtpPassword),
            EnableSsl = _enableSsl,
        };

        try
        {
            await smtpClient.SendMailAsync(message);
        }
        catch (SmtpException smtpEx)
        {
            // Log or handle SMTP errors appropriately
            Console.WriteLine($"SMTP Error: {smtpEx.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Log general exceptions
            Console.WriteLine($"Email Sending Error: {ex.Message}");
            throw;
        }
    }
}
