using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppFinalGrupoDos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        public bool IsLogedIn { get; set; }

        public Login()
        {
            _googleManager = DependencyService.Get<IGoogleManager>();
            CheckUserLoggedIn();
            InitializeComponent();
        }

        private void CheckUserLoggedIn()
        {
            _googleManager.Login(OnLoginComplete);
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                _googleManager.Login(OnLoginComplete);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }
        }

        private async void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if (googleUser != null)
            {
                GoogleUser = googleUser;
                /*lblName.Text = GoogleUser.Name;
                lblEmail.Text = GoogleUser.Email;
                imgProfile.Source = GoogleUser.Picture;*/
                IsLogedIn = true;
                await Navigation.PushAsync(new Usuario(GoogleUser.Name, GoogleUser.Email, GoogleUser.Picture));
            }
            else
            {
                await DisplayAlert("Mensaje de alerta", message, "OK");
            }
        }
        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }

        /*private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            try
            {
                _googleManager.Logout();

                lblName.Text = "Nombre";
                lblEmail.Text = "Email";
                imgProfile.Source = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }
        }*/
    }
}