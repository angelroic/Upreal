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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.IO.IsolatedStorage;

namespace LateralMenus
{
     
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        Dictionary<string, string> dtype = new Dictionary<string, string>();
        public MainPage()
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
            do_my_new();
            do_my_history();
        }
        
       private void complettype()
        {
            dtype.Add("1", "a consulte");
            dtype.Add("2", "a aimer");
            dtype.Add("3", "a pas aimer");
            dtype.Add("4", "n'aime plus");
            dtype.Add("5", "a partager");
            dtype.Add("6", "a commenter");
            dtype.Add("7", "a modifier son profil");
            dtype.Add("8", "a ajouter un produit");
        }
        async private void do_my_history()
        {
            //if (Utilisateur.isConnect == true)
            //{
         
               complettype();
                HttpClient httpClient = new HttpClient();
                List<New> lnew = new List<New>();
                httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
                HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");

                       // var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202//UpReal/services/UserUtilManager/getUserHistory?id_user=1"));
                   var Response = await httpClient.GetAsync(new Uri("http://10.224.9.202//UpReal/services/UserUtilManager/getUserHistory?id_user=1"));

                var statusCode = Response.StatusCode;

                Response.EnsureSuccessStatusCode();
                var ResponseText = await Response.Content.ReadAsStringAsync();
                XElement doc = XElement.Parse(ResponseText);
                var query = doc.Descendants();
                foreach (XElement ele in query)
                {
                    if (ele.Name.ToString().Contains("action_type"))
                    {
                        HistoryList.Items.Add(dtype[ele.Value.ToString()]);
                    }
                }
            //}
            ////else
            //{
            //    MessageBox.Show("Vous n'etes pas connecte");
            //}
        }
        async private void do_my_new()
        {
            HttpClient httpClient = new HttpClient();
            string tempo = "";
            string id_tempo = "";
            List<New> lnew = new List<New>();
            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");

           var Response = await httpClient.GetAsync(new Uri("http://10.224.9.202//UpReal/services/GlobalManager/getNews"));
         //   var Response = await httpClient.GetAsync(new Uri("http://163.5.84.202//UpReal/services/GlobalManager/getNews"));
          
            var statusCode = Response.StatusCode;

            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            XElement doc = XElement.Parse(ResponseText);
            var query = doc.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("title"))
                {
                    New p = new New() { titleNew = ele.Value, imageNew = "http://10.224.9.202/Symfony/web/images/News/" + tempo, id = id_tempo };
                    lnew.Add(p);

                }
                if (ele.Name.ToString().Contains("picture"))
                {
                    tempo = ele.Value;
                }
                if (ele.Name.ToString().Contains("id"))
                {
                    id_tempo = ele.Value;
                }
            }
            listNews.DataContext = lnew;

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

        private void RechercheBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RechercheBox.Text = "";
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

        private void listNews_DoubleTap(object sender, GestureEventArgs e)
        {
            New i = (New)listNews.SelectedItem;

            NavigationService.Navigate(new Uri("/NewPage.xaml?msg=" + i.id, UriKind.Relative));
        }

        private void Actualite_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }



}
