using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService
{
    public static void SendMail ()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress ("James Aranda", "jamesaranda777@gmail.com"));
        message.To.Add(new MailboxAddress ("Admin", "james_220600@gmail.com"));
        message.Subject = "BankConsole: Usuarios nuevos";

        message.Body = new TextPart("plain"){
            Text = GetEmailText()
        };

        using (var client = new SmtpClient ()) {
            /*Es el servidor de gmail y el puerto*/
            client.Connect("smtp.gmail.com", 587, false);
            /*gamil propociona una contraseña para enviar desde una app como openia*/
            client.Authenticate("jamesaranda777@gmail.com", "11111111111111111");
            client.Send(message);
            client.Disconnect(true);
        } 

    }

    private static string GetEmailText()
    {
        List<User> newUsers = Storage.GetNewUsers();
        if (newUsers.Count == 0)
            return "No hay usuarios nuevos.";
        string emailText = "Usuarios agregados hoy:\n";

        foreach (User user in newUsers)
            emailText += "\t+ " + user.ShowData() + "\n";
        return emailText;
    }
}