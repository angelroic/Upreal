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
using System.Threading.Tasks;


namespace LateralMenus
{
    public partial class Connect : PhoneApplicationPage
    {
        public List<string> ListValue = new List<string>();
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
            WebService web = new WebService();
            var task = web.AskWebService("UserManager/connectAccount?username=" + usernameBox.Text + "&password=" + passBox.Text);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("return"))
                {
                    if (Convert.ToBoolean(ele.Value) == true)
                    {
                        Utilisateur.appSettings.Clear();
           
                        Utilisateur.isConnect = true;
                        var b =  Find_id(usernameBox.Text);
                        await b;
                        var t = find_list(Utilisateur.id);
                        await t;
                        MessageBox.Show("Connexion reussi");
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Compte utilisateur ou mot de passe incorrect");
                    }
                }
            }
        }
        async private Task<int> find_list(int id_user)
        {
            WebService web = new WebService();
            var task = web.AskWebService("GlobalManager/getUserList?id_user=" + id_user);
            await task;
            var query = web.value.Descendants();
            list listInfo = new list();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("id") && ele.Name.ToString().Contains("id_user") == false)
                {                 
                    listInfo.id = Convert.ToInt32(ele.Value);
                    var t = complet_list(Convert.ToInt32(ele.Value));
                    await t;
                }
                if (ele.Name.ToString().Contains("name"))
                {
                    listInfo.name = ele.Value;
                }
                if (ele.Name.ToString().Contains("nb_items"))
                {
                    listInfo.nb_items = Convert.ToInt32(ele.Value);
                }
                if (ele.Name.ToString().Contains("publics"))
                {
                    listInfo.publics = Convert.ToInt32(ele.Value);
                }
                if (ele.Name.ToString().Contains("type"))
                {
                    listInfo.type = Convert.ToInt32(ele.Value);
                    Utilisateur.myList.Add(listInfo, ListValue);
                    ListValue = new List<string>();
                    listInfo = new list();
                }
            }
            return 1;
        }
        async Task<int> find_product(int id)
        {
            WebService web = new WebService();
            var task = web.AskWebService("ProductManager/getProductInfo?id=" + id);
            await task;
            var query = web.value.Descendants();
           
            foreach (XElement ele in query)
            {
                if ( ele.Name.ToString().Contains("name"))
                {
                    ListValue.Add(ele.Value);
                }
            }
            return 1;
        }
        async Task<int> complet_list(int id)
        {
            WebService web = new WebService();
            var task = web.AskWebService("GlobalManager/getItemsLists?id_list=" + id);
            await task;
            var query = web.value.Descendants();
            ListValue.Clear();
            foreach (XElement ele in query)
            {
                if(ele.Name.ToString().Contains("id_product"))
                {
                    var t = find_product(Convert.ToInt32(ele.Value));
                    await t;
                }
            }
            return 1;
        }

        async Task<int> Find_id(string username)
        {
            WebService web = new WebService();

            var task = web.AskWebService("UserManager/getUserByUsername?username=" + username);
            await task;
            var query = web.value.Descendants();
            int i = 0;
            foreach (XElement ele in query)
            {
                if (i == 5)
                {
                    Utilisateur.email = ele.Value;
                    Utilisateur.appSettings.Add("email", Utilisateur.email);
                    Utilisateur.appSettings.Save();
                }
                if (i == 6)
                {
                    Utilisateur.firstname = ele.Value;
                    Utilisateur.appSettings.Add("firstname", Utilisateur.firstname);
                    Utilisateur.appSettings.Save();
                }
                if (i == 7)
                {
                    Utilisateur.id = Convert.ToInt32(ele.Value);
                    Utilisateur.appSettings.Add("id", Utilisateur.id);
                    Utilisateur.appSettings.Save();
                }
                if (i == 8)
                {
                    Utilisateur.city = ele.Value;
                    Utilisateur.appSettings.Add("city", Utilisateur.city);
                    Utilisateur.appSettings.Save();
                }
                if (i == 9)
                {
                    Utilisateur.name = ele.Value;
                    Utilisateur.appSettings.Add("name", Utilisateur.name);
                    Utilisateur.appSettings.Save();
                }
                //if (i == 10)
                //{
                //    Utilisateur.password = ele.Value;
                //    Utilisateur.appSettings.Add("Name", Utilisateur.name);
                //}
                //if (i == 11)
                //    Utilisateur.tel = Convert.ToInt32(ele.Value);
                //short desc
                //if (i == 13)
                //    Utilisateur.email = ele.Value;
                if (i == 14)
                {
                    Utilisateur.username = ele.Value;
                    Utilisateur.appSettings.Add("username", Utilisateur.username);
                    Utilisateur.appSettings.Save();
                }
                i++;
            };
            Utilisateur.appSettings.Add("connect", Utilisateur.isConnect);
            Utilisateur.appSettings.Save();
            return 1;
        }

        async private void SendInscriptionButton_Click(object sender, RoutedEventArgs e)
        {
            WebService web = new WebService();

            var task = web.AskWebService("UserManager/registerAccount?" + "username=" + NameBox.Text + "&" + "password=" + PasswordBox.Text + "&" + "email=" + EmailBox.Text);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {

                if (ele.Name.ToString().Contains("return"))
                {
                    if (Convert.ToInt32(ele.Value) == -1)
                    {
                        MessageBox.Show("Username deja utilise");
                    }
                    else if (Convert.ToInt32(ele.Value) == -2)
                    {
                        MessageBox.Show("Email deja utilise");
                    }
                    else
                    {
                        MessageBox.Show(ele.Value);
                        MessageBox.Show("Inscription reusi");
                    }
                }
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