using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devops_Web.Models
{
    static class UsuarioAutenticado
    {
        static bool isUserLoggedIn = false;
        public static bool IsUserLoggedIn
        {
            get { return isUserLoggedIn; }
            set { isUserLoggedIn = value; }
        }

        static string userEmailouCelular = string.Empty;
        //static string userEmailouCelular = "+5511948391491";
        public static string UserEmailouCelular
        {
            get { return userEmailouCelular; }
            set { userEmailouCelular = value; }
        }

        static string userHash = string.Empty;
        //static string userHash = "a87ff679a2f3e71d9181a67b7542122c";            
        public static string UserHash
        {
            get { return userHash; }
            set { userHash = value; }
        }
    }
}
