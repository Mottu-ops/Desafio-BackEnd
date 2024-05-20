using System.Text;

namespace MotorcycleRental.MailServiceConsumer.Config
{
    public static class Layout
    {
        public static string GetMessage(string objeto)
        {

            StringBuilder msg = new StringBuilder();

            msg.AppendLine("<html><head>");
            msg.AppendLine("<meta charset=\"UTF-8\">");
            msg.AppendLine("<style> body { font - family: Arial, sans - serif; background - color: #f4f4f4; padding: 20px; }</style>");
            msg.AppendLine("</head>");
            msg.AppendLine("<body>");
            msg.AppendLine("<div class=\"container\">");
            msg.AppendLine("<h1>Olá</h1>");
            msg.AppendLine("<p> </p>");
            msg.AppendLine($"<p>Erro ao processar esse objeto:</p>");
            msg.AppendLine("<p> </p>");
            msg.AppendLine($"<p>{objeto}</p>");
            msg.AppendLine("<p> </p>");
            msg.AppendLine("<p>Atenciosamente,</p>");
            msg.AppendLine("<p>Support Tecnico Motorcycle Rental</p>");
            msg.AppendLine("</div></body></html>");

            return msg.ToString();
        }
    }
}
