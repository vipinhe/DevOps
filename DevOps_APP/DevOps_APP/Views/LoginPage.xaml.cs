using DevOps_APP.Models;
using DevOps_APP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace DevOps_APP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            //Verifica se o usuário esta logado
            if (UsuarioAutenticado.IsUserLoggedIn)
            {
                Navigation.PushAsync(new MainPage());
            }

            InitializeComponent ();
            
            EmailCelularAutenticacao.Completed += (object sender, EventArgs e) =>
            {
                SenhaAutenticacao.Focus();
            };

            var vm = new LoginViewModel();
            this.BindingContext = vm;

            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Erro", "Login ou Senha inválidos, tente novamente", "OK");
            vm.DisplayValidLoginPrompt += () => DisplayAlert("Erro", "Usuário e Senha autenticado", "OK");
            vm.DisplayPreencherLoginSenhaPrompt += () => DisplayAlert("Erro", "Os campos de Email e Senha devem ser preenchidos!", "OK");            
        }
	}
}