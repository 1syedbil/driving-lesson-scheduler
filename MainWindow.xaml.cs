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
            string emailReceiver = null;
            string password = File.ReadAllText("gmail.txt");
            if (instructor1.IsSelected)
            {
                emailReceiver = "1syedbil@gmail.com";
            }

            MailMessage lessonReminder = new MailMessage();
            lessonReminder.To.Add(emailReceiver);
            lessonReminder.From = new MailAddress(emailSender);
            lessonReminder.Body = "Hello " + instructor1.Content.ToString() + "<br>You have a lesson with " + studNameInput.Text + " on, " + lessonDateSelection.SelectedDate.ToString() + ".";
            lessonReminder.Subject = "New Lesson Scheduled";
            lessonReminder.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(emailSender, password);

            try
            {
                smtpClient.Send(lessonReminder);
                MessageBox.Show("Email has been sent.", "Success!", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
