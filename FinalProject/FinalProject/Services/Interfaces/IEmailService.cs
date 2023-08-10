using FinalProject.Helpers;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace FinalProject.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
