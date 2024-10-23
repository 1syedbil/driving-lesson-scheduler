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

        private void SendEmail(string emailSender, string emailReceiver, string messageBody)
        {
            string password = File.ReadAllText("gmail.txt");
            MailMessage email = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            email.To.Add(emailReceiver);
            email.From = new MailAddress(emailSender);
            email.Body = messageBody;
            email.Subject = "New Lesson Scheduled";
            email.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(emailSender, password);

            try
            {
                smtpClient.Send(email);
                MessageBox.Show("Email has been sent.", "Success!", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
