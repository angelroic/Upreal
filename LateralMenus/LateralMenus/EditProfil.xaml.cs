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
    public partial class EditProfil : PhoneApplicationPage
    {
        public EditProfil()
        {
            InitializeComponent();
            if (Utilisateur.firstname != "" )
            {
                PrenomBox.Text = Utilisateur.firstname;
            }
            //if (Utilisateur.secondname != "")
            //{
            //    Prenom2Box.Text = Utilisateur.secondname;
            //}
            if (Utilisateur.name != "")
            {
                NomBox.Text = Utilisateur.name;
            }
            if (Utilisateur.tel != 0)
            {
                TelBox.Text = Utilisateur.tel.ToString();
            }
            if (Utilisateur.city != "")
            {
               VilleBox.Text = Utilisateur.city;
            }
            if (Utilisateur.firstname != "")
            {
               NumeroCheck.IsChecked = Utilisateur.telcheck;
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
                NavigationService.Navigate(new Uri("/Profil.xaml", UriKind.Relative));
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

        async private void Valider_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");

            var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202/UpReal/services/UserManager/updateAccount?" + "id=" + Utilisateur.id + "&" + "firstname=" + PrenomBox.Text + "&" + "lastname=" + NomBox.Text + "&phone=" + TelBox.Text + "&id_adress=75"));
            var statusCode = Response.StatusCode;

            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            XElement doc = XElement.Parse(ResponseText);
            if (Convert.ToBoolean(doc.Value) == true)
            {
                MessageBox.Show("Changement reussi");
                Utilisateur.city = VilleBox.Text;
                Utilisateur.name = NomBox.Text;
                Utilisateur.firstname = PrenomBox.Text;
                NavigationService.Navigate(new Uri("/Profil.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void RechercheBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RechercheBox.Text = "";
        }

        private void PrenomBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PrenomBox.Text = "";
        }

        private void Prenom2Box_GotFocus(object sender, RoutedEventArgs e)
        {
            Prenom2Box.Text = "";
        }

        private void NomBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NomBox.Text = "";
        }

        private void TelBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TelBox.Text = "";
        }

        private void VilleBox_GotFocus(object sender, RoutedEventArgs e)
        {
            VilleBox.Text = "";
        }

        private void MyListe_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MyList.xaml", UriKind.Relative));    
        }

        private void CarteFidel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CarteFidel.xaml", UriKind.Relative)); 
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