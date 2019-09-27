using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DevOps_APP.Models;
using DevOps_APP.Views;
using System.Text;
using System.Collections.Generic;
using DevOps_APP.Services;


using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace DevOps_APP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command CadastrarSeCommand { get; set; }
        public Command EsqueciSenhaCommand { get; set; }
        public Command LoginCommand { get; set; }

        public Action DisplayInvalidLoginPrompt;
        public Action DisplayValidLoginPrompt;

        public Action DisplayPreencherLoginSenhaPrompt;

        LoginLoadingPopup loginLoadingPopup;
        LoginSuccessPopup loginSuccessPopup;
        CadastroPage cadastroPage;

        public string MensagemPopupAutenticacao { get; set; }

        string emailCelularAutenticacao = "111";
        public string EmailCelularAutenticacao
        {
            get { return emailCelularAutenticacao; }
            set { SetProperty(ref emailCelularAutenticacao, value); }
        }
        
        private string senhaAutenticacao = "111";
        public string SenhaAutenticacao
        {
            get { return senhaAutenticacao; }
            set { SetProperty(ref senhaAutenticacao, value); }
        }

        public LoginViewModel()
        {
            EsqueciSenhaCommand = new Command(async () => await IrParaEsqueciSenhaCommand());
            CadastrarSeCommand = new Command(async () => await IrParaCadastroCommand());
            LoginCommand = new Command(async () => await AutenticarLoginCommandAsync());

            MensagemPopupAutenticacao = "Login realizado com sucesso!";

            loginLoadingPopup = new LoginLoadingPopup();
            loginSuccessPopup = new LoginSuccessPopup(MensagemPopupAutenticacao);

            cadastroPage = new CadastroPage();
        }

        private async Task AutenticarLoginCommandAsync()
        {
            if (string.IsNullOrWhiteSpace(EmailCelularAutenticacao) || string.IsNullOrWhiteSpace(SenhaAutenticacao))
            {
                DisplayPreencherLoginSenhaPrompt();
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;       

            await UseLoginLoadingPopup(true);

            await Task.Delay(2000);

            var AutenticarUsuario = new LoginUsuario
            {
                EmailCelularAutenticacao = EmailCelularAutenticacao,
                SenhaAutenticacao = md5Hash.GetMd5Hash(SenhaAutenticacao) 
            };

            try
            {
                var AutenticacaoResult = await MockDataStoreUsuarios.AutenticarUsuarioAsync(AutenticarUsuario);

                if (string.IsNullOrEmpty(AutenticacaoResult))
                {
                    await UseLoginLoadingPopup(false);

                    DisplayInvalidLoginPrompt();

                    SenhaAutenticacao = string.Empty;

                    return;
                }

                var Usuario = await MockDataStoreUsuarios.BuscarDadosUsuarioAsync(AutenticacaoResult);

                if (!string.IsNullOrEmpty(AutenticacaoResult) && Usuario != null)
                {                    
                    await UseLoginSuccessPopup();
                    await UseLoginLoadingPopup(false);

                    UsuarioAutenticado.IsUserLoggedIn = true;
                    UsuarioAutenticado.UserHash = AutenticacaoResult;

                    await Task.Delay(1000);

                    await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await UseLoginLoadingPopup(false);

                    DisplayInvalidLoginPrompt();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {               
                IsBusy = false;
            }
        }

        private async Task IrParaCadastroCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(cadastroPage);
        }

        private Task IrParaEsqueciSenhaCommand()
        {
            throw new NotImplementedException();
        }

        //Popup Sucess
        public async Task UseLoginSuccessPopup()
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync(loginSuccessPopup);            
        }

        //Popup Loading
        public async Task UseLoginLoadingPopup(bool PopPush)
        {
            if (PopPush)
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(loginLoadingPopup);
            }
            else
            {
                await Application.Current.MainPage.Navigation.RemovePopupPageAsync(loginLoadingPopup);
            }
        }
    }
}