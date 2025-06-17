using DevHobby.GPTizza.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace DevHobby.GPTizza.Components.Account;

// Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailSender emailSender = new NoOpEmailSender();

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        emailSender.SendEmailAsync(email, "Potwierdź swój adres e-mail", $"Proszę potwierdzić swoje konto przez <a href='{confirmationLink}'>klikając tutaj</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        emailSender.SendEmailAsync(email, "Zresetuj swoje hasło", $"Proszę zresetować hasło poprzez <a href='{resetLink}'>klikając tutaj</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        emailSender.SendEmailAsync(email, "Zresetuj swoje hasło", $"Zresetuj hasło za pomocą poniższego kodu: {resetCode}");
}
