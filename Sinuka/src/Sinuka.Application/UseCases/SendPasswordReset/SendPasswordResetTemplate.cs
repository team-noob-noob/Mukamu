namespace Sinuka.Application.UseCases.SendPasswordReset
{
    public class SendPasswordResetTemplate
    {
        public static string Template(string link)
            => $"<h1>Password Reset Link</h1><br><a href='{link}'>{link}</a>";
    }
}
