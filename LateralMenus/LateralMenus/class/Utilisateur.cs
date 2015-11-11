using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;

namespace LateralMenus
{
    public static class Utilisateur
    {
        public static IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
        public static Boolean isConnect = false;
        internal static Dictionary<string, List<string>> myList = new Dictionary<string, List<string>>();
        public static string username { get; set; } 
        public static string email { get; set; }
        public static string password { get; set; }
        public static string firstname { get; set; }

        public static string secondname { get; set; }
        public static int id {get; set;} 
        public static int tel { get; set; }
        public static string city { get; set; }
        public static string name { get; set; }
        public static bool telcheck { get; set; }

        
    }
}
