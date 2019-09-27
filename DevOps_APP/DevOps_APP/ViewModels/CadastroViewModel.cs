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
using System.Linq;

namespace DevOps_APP.ViewModels
{
    public class CadastroViewModel : BaseViewModel
    {
        //Finalizar Cadastro
        public Command FinalizarCadastroCommand { get; set; }
        public Command PossuoUmaContaCommand { get; set; }

        public Action DisplayImpossivelCadastrarPrompt;
        public Action DisplayPreencherCamposPrompt;
        public Action DisplayDigiteEmailCelularValidoPrompt;

        Usuario NovoUsuarioDadosCadastro;

        LoginLoadingPopup loginLoadingPopup;
        LoginSuccessPopup loginSuccessPopup;

        public string MensagemPopupValidacao { get; set; }

        //Inicio propriedades
        string nomeCadastroValidacao;
        public string NomeCadastroValidacao
        {
            get { return nomeCadastroValidacao; }
            set { SetProperty(ref nomeCadastroValidacao, value); }
        }

        private string sobrenomeCadastroValidacao;
        public string SobrenomeCadastroValidacao
        {
            get { return sobrenomeCadastroValidacao; }
            set { SetProperty(ref sobrenomeCadastroValidacao, value); }
        }

        private DateTime dataNascimentoCadastroValidacao = DateTime.Today;
        public DateTime DataNascimentoCadastroValidacao
        {
            get { return dataNascimentoCadastroValidacao; }
            set { SetProperty(ref dataNascimentoCadastroValidacao, value); }
        }    

        private DateTime dataMaximaNascimento = DateTime.Today;
        public DateTime DataMaximaNascimento
        {
            get { return dataMaximaNascimento; }
        }
                
        private DateTime dataMinimaNascimento = new DateTime(1900, 01, 01);
        public DateTime DataMinimaNascimento
        {
            get { return dataMinimaNascimento; }
        }            

        private string mostrarDataEntryValidacao;
        public string MostrarDataEntryValidacao
        {
            get { return mostrarDataEntryValidacao; }
            set { SetProperty(ref mostrarDataEntryValidacao, value); }
        }

        private string emailCelularCadastroValidacao;
        public string EmailCelularCadastroValidacao
        {
            get { return emailCelularCadastroValidacao; }
            set { SetProperty(ref emailCelularCadastroValidacao, value); }
        }

        private string senhaCadastroValidacao;
        public string SenhaCadastroValidacao
        {
            get { return senhaCadastroValidacao; }
            set { SetProperty(ref senhaCadastroValidacao, value); }
        }

        public CadastroViewModel()
        {
            //Finalizar Cadastro
            FinalizarCadastroCommand = new Command(async () => await IrParaFinalizarCadastroCommand());
            PossuoUmaContaCommand = new Command(async () => await IrParaLoginPageCommand());

            MensagemPopupValidacao = "Cadastro realizado com sucesso!";

            loginLoadingPopup = new LoginLoadingPopup();
            loginSuccessPopup = new LoginSuccessPopup(MensagemPopupValidacao);
        }

        private async Task IrParaLoginPageCommand()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        //Finalizar Cadastro
        private async Task IrParaFinalizarCadastroCommand()
        {
            if (string.IsNullOrWhiteSpace(NomeCadastroValidacao) ||
                string.IsNullOrWhiteSpace(SobrenomeCadastroValidacao) ||
                DataNascimentoCadastroValidacao == null ||
                string.IsNullOrWhiteSpace(EmailCelularCadastroValidacao) ||
                string.IsNullOrWhiteSpace(SenhaCadastroValidacao))
            {
                DisplayPreencherCamposPrompt();
                return;
            }   

            if (EmailCelularCadastroValidacao.Contains("@"))
            {
                NovoUsuarioDadosCadastro = new Usuario
                {
                    Nome = NomeCadastroValidacao,
                    SobreNome = SobrenomeCadastroValidacao,
                    DataNascimento = DataNascimentoCadastroValidacao,
                    Email = EmailCelularCadastroValidacao,
                    Senha = md5Hash.GetMd5Hash(SenhaCadastroValidacao)
                };
            }
            else if (EmailCelularCadastroValidacao.All(char.IsDigit))
            {
                NovoUsuarioDadosCadastro = new Usuario
                {
                    Nome = NomeCadastroValidacao,
                    SobreNome = SobrenomeCadastroValidacao,
                    DataNascimento = DataNascimentoCadastroValidacao,
                    Celular = EmailCelularCadastroValidacao,
                    Senha = md5Hash.GetMd5Hash(SenhaCadastroValidacao)
                };
            }
            else
            {
                await UseLoginLoadingPopup(false);
                DisplayDigiteEmailCelularValidoPrompt();
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;

            await UseLoginLoadingPopup(true);

            await Task.Delay(2000);

            try
            {
                var CadastroResult = await MockDataStoreUsuarios.AdicionarUsuario(NovoUsuarioDadosCadastro);

                if (CadastroResult)
                {
                    await UseLoginSuccessPopup();
                    await UseLoginLoadingPopup(false);

                    await Task.Delay(1000);

                    NomeCadastroValidacao = string.Empty;
                    SobrenomeCadastroValidacao = string.Empty;
                    MostrarDataEntryValidacao = string.Empty;
                    EmailCelularCadastroValidacao = string.Empty;
                    SenhaCadastroValidacao = string.Empty;

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await UseLoginLoadingPopup(false);

                    DisplayImpossivelCadastrarPrompt();
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

        //Popup Sucess
        public async Task UseLoginSuccessPopup()
        {
            await Application.Current.MainPage.Navigation.PushPopupAsync(loginSuccessPopup);            
        }    
    }
}