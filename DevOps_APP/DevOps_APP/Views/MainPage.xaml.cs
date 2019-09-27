using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevOps_APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

            OnBackButtonPressed();

            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return false;
        }
    }
}