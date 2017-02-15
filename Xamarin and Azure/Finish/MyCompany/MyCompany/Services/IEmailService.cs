using Plugin.Messaging;

namespace MyCompany
{
	public interface IEmailService
	{
		bool CanSendEmail { get; }
		bool CanSendEmailAttachments { get; }
		bool CanSendEmailBodyAsHtml { get; }
		void SendEmail(IEmailMessage email);
		void SendEmail(string to, string subject, string message);
	}
}
