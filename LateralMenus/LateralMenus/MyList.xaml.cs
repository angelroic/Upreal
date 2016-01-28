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
using Microsoft.Phone.Tasks;

namespace LateralMenus
{
    public partial class MyList : PhoneApplicationPage
    {
        public MyList()
        {
            InitializeComponent();
           
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
      

        private void RechercheBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RechercheBox.Text = "";
        }

        private void MyList_Click(object sender, RoutedEventArgs e)
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

        async private void AjouterListButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxList.Text != "")
            {
                WebService web = new WebService();
                var task = web.AskWebService("GlobalManager/createLists?name=" + TextBoxList.Text + "&publics=1" + "&type=8" + "&nb_items=0"+  "&id_user=" + Utilisateur.id);
                await task;
                var query = web.value.Descendants();
                foreach (XElement ele in query)
                {
                    if (ele.Name.ToString().Contains("return"))
                    {
                        list l = new list();
                        l.id = Convert.ToInt32(ele.Value);
                        l.name = TextBoxList.Text;
                        l.publics = 1;
                        l.type = 8;
                        l.nb_items = 0;
                        List<string> t = new List<string>();
                        Utilisateur.myList.Add(l, t);
                        ListItem.Items.Add(l.name);
                    }
                }
            }
            else
            {
                MessageBox.Show("veuillez choisir un nom de liste");
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ListItem.Items.Clear(); 
            foreach (list key in Utilisateur.myList.Keys)
            {
                ListItem.Items.Add(key.name);
            }
        }

        private void ListItem_DoubleTap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ListContent.xaml?msg=" + ListItem.SelectedItem, UriKind.Relative));
        }

        private void TextBoxList_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}