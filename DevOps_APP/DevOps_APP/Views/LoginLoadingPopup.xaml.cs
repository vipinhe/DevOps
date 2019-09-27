using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace DevOps_APP.Views
{
	public partial class LoginLoadingPopup : PopupPage
	{
		public LoginLoadingPopup ()
		{
			InitializeComponent ();
		}

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}