using DevOps_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevOps_APP.Models
{
    static class UsuarioAutenticado
    {
        static bool isUserLoggedIn = false;
        public static bool IsUserLoggedIn
        {
            get { return isUserLoggedIn; }
            set { isUserLoggedIn = value; }
        }

        static string userHash = string.Empty;
        public static string UserHash
        {
            get { return userHash; }
            set { userHash = value; }
        }
    }
}
