using wellmanage.application.Models;

namespace wellmanage.application.Interfaces;

public interface IEmailService
{
      Task SendEmail(Message message);
}