using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace drivingLessonScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void submitLesson_Click(object sender, RoutedEventArgs e)
        {
            string emailSender = "continentalautomessage@gmail.com";
            string messageBody = "Hello " + instructor1.Content.ToString() + ",<br>You have a " + lessonType1.Content.ToString() + " with " + studNameInput.Text + " on, " + lessonDateSelection.SelectedDate.ToString() + ". Their phone number is, " + phoneNumInput.Text + ".";
            string emailReceiver = null;

            if (instructor1.IsSelected)
            {
                emailReceiver = "1syedbil@gmail.com";
            }

            SendEmail(emailSender, emailReceiver, messageBody);
        }

        private async Task SendEmail(string emailSender, string emailReceiver, string messageBody)
        {
            string password = File.ReadAllText("gmail.txt");
            MailMessage email = CreateMessage(emailReceiver, emailSender, messageBody);
            MailMessage text = CreateMessage("6476680648@txt.bell.ca", emailSender, messageBody);
            SmtpClient smtpClient1 = CreateClient(emailSender, password);
            SmtpClient smtpClient2 = CreateClient(emailSender, password);

            try
            {
                lessonForm.Visibility = Visibility.Collapsed;
                loadingOverlay.Visibility = Visibility.Visible;
                await smtpClient1.SendMailAsync(email);
                await smtpClient2.SendMailAsync(text);
                loadingOverlay.Visibility = Visibility.Collapsed;
                lessonForm.Visibility = Visibility.Visible;
                MessageBox.Show("Email has been sent.", "Success!", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                loadingOverlay.Visibility = Visibility.Collapsed;
                lessonForm.Visibility = Visibility.Visible;
                MessageBox.Show(ex.Message);
            }
        }

        private MailMessage CreateMessage(string emailReceiver, string emailSender, string messageBody)
        {
            MailMessage message = new MailMessage();

            message.To.Add(emailReceiver);
            message.From = new MailAddress(emailSender);
            message.Body = messageBody;
            message.Subject = "New Lesson Scheduled";
            message.IsBodyHtml = true;

            return message;
        }

        private SmtpClient CreateClient(string emailSender, string password)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");

            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(emailSender, password);

            return client;
        }
    }
}
