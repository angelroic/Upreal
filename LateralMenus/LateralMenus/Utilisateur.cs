using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LateralMenus
{
    public static class Utilisateur
    {
        public static Boolean isConnect = false;
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
        //public Utilisateur()
        //{
        //    username = "";
        //    email = "";
        //    password = "";
        //    firstname = "";
        //    secondname = "";
        //    tel = 0;
        //    city = "";
        //    name = "";
        //    id = 0;
        //    telcheck = true;
        //}
        
    }
}
