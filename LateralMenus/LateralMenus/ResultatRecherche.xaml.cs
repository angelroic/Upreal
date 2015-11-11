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

    public partial class ResultatRecherche : PhoneApplicationPage
    {
        string object_name = "";

        string tempo = "";
        public ResultatRecherche()
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
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("msg", out object_name))
            {
                List<Item> List = new List<Item>();
                WebService web = new WebService();

                var task = web.AskWebService("ProductManager/getProduct?keyword=" + object_name);
                await task;
                var query = web.value.Descendants();
                string tempo = "";
                foreach (XElement ele in query)
                {
                    if (ele.Name.ToString().Contains("name"))
                    {
                        tempo = ele.Value;
                        
                    }
                    if (ele.Name.ToString().Contains("picture"))
                    {

                        Item p = new Item() { name = tempo, imagePath = Img.ecole +"Product/" + ele.Value };
                        List.Add(p);
                    }
                };
                ProduitList.DataContext = List;
                DoMyStoreList(object_name);
                DoMyBrand(object_name);
                DoMyUtilisateur(object_name);
            }
        }
        async private void DoMyUtilisateur(string object_name)
        {
            List<Item> List1 = new List<Item>();
            WebService web = new WebService();

            var task = web.AskWebService("UserManager/getUserByUsername?username=" + object_name);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("username"))
                {

                    Item p = new Item() { name = ele.Value, imagePath = Img.ecole+ "user/" + tempo };
                    List1.Add(p);

                }
                if (ele.Name.ToString().Contains("picture"))
                {
                    tempo = ele.Value;
                }
            }
            UtilisateurList.DataContext = List1;

        }
        async private void DoMyBrand(string object_name)
        {
            List<Item> List2 = new List<Item>();
            WebService web = new WebService();

            var task = web.AskWebService("CompanyManager/getCompanyByName?keyword=" + object_name);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                    if (ele.Name.ToString().Contains("name"))
                    {
                        tempo = ele.Value;
                        
                    }
                    if (ele.Name.ToString().Contains("picture"))
                    {

                        Item p = new Item() { name = tempo, imagePath = Img.ecole + "Product/" + ele.Value };
                        List2.Add(p);
                    }
                };
            MarqueList.DataContext = List2;
        }
        async private void DoMyStoreList(string object_name)
        {
            List<Item> List3 = new List<Item>();
            WebService web = new WebService();

            var task = web.AskWebService("StoreManager/getStoreByName?keyword=" + object_name);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("name"))
                {
                    tempo = ele.Value;

                }
                if (ele.Name.ToString().Contains("picture"))
                {

                    Item p = new Item() { name = tempo, imagePath =  Img.ecole+ "Store/" + ele.Value };
                    List3.Add(p);
                }
            };
            StoreList.DataContext = List3;
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

        private void ProduitList_DoubleTap(object sender, GestureEventArgs e)
        {
            Item i = (Item)ProduitList.SelectedItem;
            
            NavigationService.Navigate(new Uri("/ItemProfil.xaml?msg=" +  i.name, UriKind.Relative));
        }

        private void StoreList_DoubleTap(object sender, GestureEventArgs e)
        {
            Item i = (Item)StoreList.SelectedItem;
            
            NavigationService.Navigate(new Uri("/StoreProfil.xaml?msg=" + i.name, UriKind.Relative));
        }

        private void Actualite_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }




    }
}