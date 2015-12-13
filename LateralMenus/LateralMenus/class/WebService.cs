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
using System.Threading.Tasks;
namespace LateralMenus
{
    class WebService
    {
        public string ecole = "http://10.224.9.202//UpReal/services/";
        public string maison = "http://163.5.84.202//UpReal/services/";
        public XElement value;
        async public Task<int> AskWebService(string web)
        {
            HttpClient httpClient = new HttpClient();
            List<New> lnew = new List<New>();
            httpClient.DefaultRequestHeaders.Accept.TryParseAdd("text/xml");
            HttpContent content = new StringContent("", Encoding.UTF8, "text/xml");
            var Response = await httpClient.GetAsync(new Uri(ecole  + web));
            var statusCode = Response.StatusCode;
            Response.EnsureSuccessStatusCode();
            var ResponseText = await Response.Content.ReadAsStringAsync();
            value = XElement.Parse(ResponseText);
            return 1;
        }
    }
}
