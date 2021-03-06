﻿using System;
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
using System.Threading.Tasks;

namespace LateralMenus
{
    public partial class StoreProfil : PhoneApplicationPage
    {
        string item_name = "";
        string tmp = "";
        public StoreProfil()
        {
            InitializeComponent();
        }
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("msg", out item_name))
            {
                WebService web = new WebService();
                var task = web.AskWebService("StoreManager/getStoreByName?keyword=" + item_name);
                await task;
                var query = web.value.Descendants();
                foreach (XElement ele in query)
                {
                    if (ele.Name.ToString().Contains("name"))
                    {
                        NomStore.Text = ele.Value;
                    }
                    else if (ele.Name.ToString().Contains("website"))
                    {
                        CateText.Text = ele.Value;
                    }
                    else if (ele.Name.ToString().Contains("picture"))
                    {
                        ImageStore.Source = new BitmapImage(new Uri(Img.ecole + "Store/" + ele.Value, UriKind.Absolute));
                    }
                    else if (ele.Name.ToString().Contains("id") && !ele.Name.ToString().Contains("address") && !ele.Name.ToString().Contains("company"))
                    {
                        get_product(ele.Value);
                    }
                }
            }
        }
        async private void get_product(string id)
        {
            WebService web = new WebService();
          
            var task = web.AskWebService("ProductUtilManager/getProductByStore?id=" + id);
            await task;
            var query = web.value.Descendants();
            ObservableCollection<Item> Product = new ObservableCollection<Item>();
            
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("id_product"))
                {
                     var i = find_product_name(ele.Value);
                     await i;
                }
                if (ele.Name.ToString().Contains("price"))
                {
                    Product.Add(new Item() { name = tmp, price = ele.Value});
                }
            }
            ProductList.ItemsSource = Product;
        }

        async private Task<int> find_product_name(string id)
        {
            WebService web = new WebService();
            var task= web.AskWebService("ProductManager/getProductInfo?id=" + id);
            await task;
            var query = web.value.Descendants();
            foreach (XElement ele in query)
            {
                if (ele.Name.ToString().Contains("name"))
                {
                    tmp = ele.Value;
                }
            }
            return 1;
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

        private void AimeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Actualite_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}