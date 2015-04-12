using System.Net.Mail;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Net;
namespace SportsStore.Domain.Concrete {
    public class EmailSettings {
        public string MailToAddress = "de_generator@hotmail.com";
        public string MailFromAddress = "frankchau93@gmail.com";
        public bool UseSsl = true;
        public string Username = "frankchau93@gmail.com";
        public string Password = "Objection!";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587; //587
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
}
    public class EmailOrderProcessor :IOrderProcessor {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings) {
            emailSettings = settings;
        }
    public void Test(Cart cart, ShippingDetails shippingInfo)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = emailSettings.ServerName;
            client.Port = emailSettings.ServerPort;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(emailSettings.Username, emailSettings.Password);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");
            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                    line.Product.Name,
                    subtotal);
            }
            body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Ship to:")
                .AppendLine(shippingInfo.Name)
                .AppendLine(shippingInfo.Line1)
                .AppendLine(shippingInfo.Line2 ?? "")
                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.State ?? "")
                .AppendLine(shippingInfo.Country)
                .AppendLine(shippingInfo.Zip)
                .AppendLine("---")
                .AppendFormat("Gift wrap: {0}",
                    shippingInfo.GiftWrap ? "Yes" : "No");
                    body.ToString(); // Body

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("frankchau93@gmail.com");
            msg.To.Add(new MailAddress("de_generator@hotmail.com"));

            msg.Subject = "New Order Submitted";
            msg.IsBodyHtml = true;
            msg.Body = body.ToString();

            
                client.Send(msg);
                
        
        }

    public void ProcessOrder(Cart cart, ShippingDetails shippingInfo) {
    
        using (var smtpClient = new SmtpClient()) {
        smtpClient.EnableSsl = emailSettings.UseSsl;
        smtpClient.Host = emailSettings.ServerName;
        smtpClient.Port = emailSettings.ServerPort;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials
            = new NetworkCredential(emailSettings.Username,
            emailSettings.Password);
            if (emailSettings.WriteAsFile) {
            smtpClient.DeliveryMethod
            = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
            smtpClient.EnableSsl = false;
        }   
            StringBuilder body = new StringBuilder()
                .AppendLine("A new order has been submitted")
                .AppendLine("---")
                .AppendLine("Items:");
            foreach (var line in cart.Lines) {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                    line.Product.Name,
                    subtotal);
                }
            body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Ship to:")
                .AppendLine(shippingInfo.Name)
                .AppendLine(shippingInfo.Line1)
                .AppendLine(shippingInfo.Line2 ?? "")
                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.State ?? "")
                .AppendLine(shippingInfo.Country)
                .AppendLine(shippingInfo.Zip)
                .AppendLine("---")
                .AppendFormat("Gift wrap: {0}",
                    shippingInfo.GiftWrap ? "Yes" : "No");
            MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress, // From
                    emailSettings.MailToAddress, // To
                    "New order submitted!", // Subject
                    body.ToString()); // Body
            if (emailSettings.WriteAsFile)
            {
                mailMessage.BodyEncoding = Encoding.ASCII;
            }
            smtpClient.Send(mailMessage);
            }
        }
    }
}