namespace Sinuka.Application.UseCases.Register
{
    public class VerifyEmailTemplate
    {
        public static string Template(string link) => @$"<h1>Link to the Verification</h1><br><a href='{link}'>{link}</a>";
    }
}