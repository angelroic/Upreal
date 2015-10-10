using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LateralMenus.Resources;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;

namespace LateralMenus
{
    public partial class Connect : PhoneApplicationPage
    {
        public Connect()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "Normal", false);
            if (Utilisateur.isConnect == false)
            {
                ConnexionButton.Content = "Connexion";
            }
            else
            {
                ConnexionButton.Content = "Mon profil";
            }
        }
        private void OpenClose_Left(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -100)
            {
                //ApplicationBar.IsVisible = true;
                MoveViewWindow(-420);
            }
            else
            {
                //ApplicationBar.IsVisible = false;
                MoveViewWindow(0);
            }
        }
        private void OpenClose_Right(object sender, RoutedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (left > -520)
            {
                //ApplicationBar.IsVisible = false;
                MoveViewWindow(-840);
            }
            else
            {
                //ApplicationBar.IsVisible = true;
                MoveViewWindow(-420);

            }
        }

        void MoveViewWindow(double left)
        {
            _viewMoved = true;
            //if (left==-420)
            //    ApplicationBar.IsVisible = true;
            //else
            //    ApplicationBar.IsVisible = false;
            ((Storyboard)canvas.Resources["moveAnimation"]).SkipToFill();
            ((DoubleAnimation)((Storyboard)canvas.Resources["moveAnimation"]).Children[0]).To = left;
            ((Storyboard)canvas.Resources["moveAnimation"]).Begin();
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.DeltaManipulation.Translation.X != 0)
                Canvas.SetLeft(LayoutRoot, Math.Min(Math.Max(-840, Canvas.GetLeft(LayoutRoot) + e.DeltaManipulation.Translation.X), 0));
        }

        double initialPosition;
        bool _viewMoved = false;
        private void canvas_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            _viewMoved = false;
            initialPosition = Canvas.GetLeft(LayoutRoot);
        }

        private void canvas_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var left = Canvas.GetLeft(LayoutRoot);
            if (_viewMoved)
                return;
            if (Math.Abs(initialPosition - left) < 100)
            {
                //bouncing back
                MoveViewWindow(initialPosition);
                return;
            }
            //change of state
            if (initialPosition - left > 0)
            {
                //slide to the left
                if (initialPosition > -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(-840);
            }
            else
            {
                //slide to the right
                if (initialPosition < -420)
                    MoveViewWindow(-420);
                else
                    MoveViewWindow(0);
            }

        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ResultatRecherche.xaml?msg=" + RechercheBox.Text, UriKind.Relative));

        }

        private void ConnexionButton_Click(object sender, RoutedEventArgs e)
        {
            if (Utilisateur.isConnect == false)
                NavigationService.Navigate(new Uri("/Connect.xaml", UriKind.Relative));
            else
            {
                MessageBox.Show("ok tu es co mais j ai pas fini ");
            }
        }

        async private void SendConnexionButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");

            var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202/UpReal/services/UserManager/connectAccount?username=" + usernameBox.Text + "&password=" + passBox.Text));
            //var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202/UpReal/services/UserManager/connectAccount?username=popo&password=pipi"));

            //(new Uri(url), content);
            var statusCode = Response.StatusCode;

            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            XElement doc = XElement.Parse(ResponseText);
            if (Convert.ToBoolean(doc.Value) == true)
            {
                MessageBox.Show("Connexion reussi");
                Utilisateur.isConnect = true;
                Find_id(usernameBox.Text);
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Compte utilisateur ou mot de passe incorrect");
            }
        }

        async void Find_id(string username)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");

            var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202/UpReal/services/UserManager/getUserByUsername?username=" + username));

            var statusCode = Response.StatusCode;

            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            XElement doc = XElement.Parse(ResponseText);
            var query = doc.Descendants();
            int i = 0;
            foreach (XElement ele in query)
            {
                if (i == 5)
                    Utilisateur.email = ele.Value;
                if (i == 6)
                    Utilisateur.firstname = ele.Value;
                if (i == 7)
                    Utilisateur.id = Convert.ToInt32(ele.Value);
                if (i == 8)
                    Utilisateur.city = ele.Value;
                if (i == 9)
                    Utilisateur.name = ele.Value;
                if (i == 10)
                    Utilisateur.password = ele.Value;
                //if (i == 11)
                //    Utilisateur.tel = Convert.ToInt32(ele.Value);
                //short desc
                //if (i == 13)
                //    Utilisateur.email = ele.Value;
                if (i == 14)
                    Utilisateur.username = ele.Value;
                i++;
            };
        }

        async private void SendInscriptionButton_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");

            var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202/UpReal/services/UserManager/registerAccount?" + "username=" + NameBox.Text + "&" + "password=" + PasswordBox.Text + "&" + "email=" + EmailBox.Text));

            var statusCode = Response.StatusCode;

            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            XElement doc = XElement.Parse(ResponseText);
            if (Convert.ToInt32(doc.Value) == -1)
            {
                MessageBox.Show("Username deja utilise");
            }
            else if (Convert.ToInt32(doc.Value) == -2)
            {
                MessageBox.Show("Email deja utilise");
            }
            else
            {
                MessageBox.Show("Inscription reusi");
            }
        }
        private void RankButton1_Click(object sender, RoutedEventArgs e)
        {
            RankButton1.IsChecked = true;
            RankButton2.IsChecked = false;
            RankButton3.IsChecked = false;
            RankButton4.IsChecked = false;
            RankButton5.IsChecked = false;
        }

        private void RankButton2_Click(object sender, RoutedEventArgs e)
        {
            RankButton1.IsChecked = false;
            RankButton2.IsChecked = true;
            RankButton3.IsChecked = false;
            RankButton4.IsChecked = false;
            RankButton5.IsChecked = false;
        }

        private void RankButton3_Click(object sender, RoutedEventArgs e)
        {
            RankButton1.IsChecked = false;
            RankButton2.IsChecked = false;
            RankButton3.IsChecked = true;
            RankButton4.IsChecked = false;
            RankButton5.IsChecked = false;
        }

        private void RankButton4_Click(object sender, RoutedEventArgs e)
        {
            RankButton1.IsChecked = false;
            RankButton2.IsChecked = false;
            RankButton3.IsChecked = false;
            RankButton4.IsChecked = true;
            RankButton5.IsChecked = false;
        }

        private void RankButton5_Click(object sender, RoutedEventArgs e)
        {
            RankButton1.IsChecked = false;
            RankButton2.IsChecked = false;
            RankButton3.IsChecked = false;
            RankButton4.IsChecked = false;
            RankButton5.IsChecked = true;
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NameBox.Text = "";
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.Text = "";
        }

        private void RePasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RePasswordBox.Text = "";
        }

        private void EmailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailBox.Text = "";
        }

        private void usernameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameBox.Text = "";
        }

        private void passBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passBox.Text = "";
        }

        private void MyListe_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NyList.xaml", UriKind.Relative));
        }


        private void MyScan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/scan.xaml", UriKind.Relative));
        }

        private void Parametre_Click(object sender, RoutedEventArgs e)
        {
            if (Utilisateur.isConnect == false)
                MessageBox.Show("Vous n'etes pas connecte");
            else
            {
                NavigationService.Navigate(new Uri("/Parametre.xaml", UriKind.Relative));
            }
        }

        private void Actualite_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }







    }
}