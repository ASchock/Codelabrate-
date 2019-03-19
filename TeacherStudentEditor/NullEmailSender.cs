using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace TeacherStudentEditor
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.FromResult(true);
        }
    }
}
