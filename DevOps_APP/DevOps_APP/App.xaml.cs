using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevOps_APP.Views;
using DevOps_APP.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DevOps_APP
{
    public partial class App : Application
    {        
        public App()
        {            
            InitializeComponent();

            UsuarioAutenticado.IsUserLoggedIn = false;

            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new CadastroPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
