using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace DevOps_APP.Views
{
	public partial class LoginSuccessPopup : PopupPage
	{
		public LoginSuccessPopup (string MensagemPopupRecebida)
		{            
            InitializeComponent ();
            MensagemPopup.Text = MensagemPopupRecebida;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(2000);
            await PopupNavigation.Instance.RemovePageAsync(this);
        }
    }   
}