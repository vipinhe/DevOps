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
	public partial class CadastroPage : ContentPage
	{
		public CadastroPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            //Verifica se o usuário esta logado
            if (UsuarioAutenticado.IsUserLoggedIn)
            {
                Navigation.PushAsync(new MainPage());
            }

            InitializeComponent ();

            NomeCadastro.Completed += (object sender, EventArgs e) =>
            {
                SobrenomeCadastro.Focus();
            };

            SobrenomeCadastro.Completed += (object sender, EventArgs e) =>
            {
                MostrarDataEntry.Focus();
            };

            MostrarDataEntry.Focused += (s, e) =>
            {
                DataNascimentoCadastro.Focus();
            };

            DataNascimentoCadastro.Unfocused += (s, e) =>
            {
                MostrarDataEntry.Text = DataNascimentoCadastro.Date.ToString("dd/MM/yyyy");
                EmailCelularCadastro.Focus();
            };

            EmailCelularCadastro.Completed += (object sender, EventArgs e) =>
            {
                SenhaCadastro.Focus();
            };

            var vm = new CadastroViewModel();
            this.BindingContext = vm;

            vm.DisplayImpossivelCadastrarPrompt += () => DisplayAlert("Erro", "Não foi possível realizar o cadastro, favor tente mais tarde!", "OK");
            vm.DisplayDigiteEmailCelularValidoPrompt += () => DisplayAlert("Aviso", "Favor Digite um email ou celular valido!", "OK");
            vm.DisplayPreencherCamposPrompt += () => DisplayAlert("Aviso", "Todos os campos do formulário devem ser preenchidos!", "OK");            
        }
    }
}