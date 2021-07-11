namespace Sinuka.Application.UseCases.PasswordReset
{
    public class PasswordResetInput
    {
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
