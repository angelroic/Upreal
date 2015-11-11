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


namespace LateralMenus
{
    public partial class ItemProfil : PhoneApplicationPage
    {
        string item_name = "";
        string id_product = "";

        public ItemProfil()
        {
            InitializeComponent();
        }
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("msg", out item_name))
            {
                WebService web = new WebService();
                var task = web.AskWebService("ProductManager/getProductByName?name=" + item_name);
                await task;
                var query = web.value.Descendants();
                foreach (XElement ele in query)
                {
                    if (ele.Name.ToString().Contains("name"))
                    {
                        NomProduit.Text = ele.Value;
                    }
                    else if (ele.Name.ToString().Contains("brand"))
                    {
                        CateText.Text = ele.Value;
                    }
                    else if (ele.Name.ToString().Contains("picture"))
                    {
                        ImageProduit.Source = new BitmapImage(new Uri(Img.ecole + "Product/" + ele.Value, UriKind.Absolute));
                    }
                    else if (ele.Name.ToString().Contains("id"))
                    {
                        id_product = ele.Value;
                        do_my_desc(ele.Value);
                        do_my_comment(ele.Value);
                    }
                };

            }

        }

        async private void do_my_comment(string id)
        {
            string tempo = "";
            WebService web = new WebService();

            var task = web.AskWebService("GlobalManager/getRate?id_target=" + id + "&id_target_type=2");
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("commentary"))
                {
                    tempo = ele.Value;
                }
                if (ele.Name.ToString().Contains("id_user"))
                {
                    if (tempo != "")
                    {
                        do_rate_comment(ele.Value, id, tempo);
                    }
                }
            }
        }

        async private void do_rate_comment(string id_user, string id, string comment)
        {
            WebService web = new WebService();
            var task = web.AskWebService("GlobalManager/getRateStatus?id_user=" + id_user + "&id_target=" + id + "&id_target_type=2");
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("return"))
                {
                    CommentList.Items.Add(comment + "\t" + ele.Value);
                }
            }
        }
        async private void do_my_desc(string id)
        {
            WebService web = new WebService();
            var task = web.AskWebService("ProductUtilManager/getDescription?id_product=" + id);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("field_desc"))
                {
                    desc.Text = ele.Value;
                }
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

        async private void AimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Utilisateur.isConnect == true)
            {
                WebService web = new WebService();

                var task = web.AskWebService("GlobalManager/likeSomething?id_user=" + "1" + "&id_target=" + id_product + "&id_target_type=2");
                await task;
                var query = web.value.Descendants();
                foreach (XElement ele in query)
                {
                    if (ele.Name.ToString().Contains("return"))
                    {
                        if (Convert.ToBoolean(ele.Value) == true)
                        {
                            MessageBox.Show("Produit aimer");
                        }
                    }
                  
                }
            }
            else
            {
                MessageBox.Show("Vous n'etes pas connecte");
            }
        }

        private void Actualite_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Comment.xaml?msg=" + id_product, UriKind.Relative));
        }

    }
}