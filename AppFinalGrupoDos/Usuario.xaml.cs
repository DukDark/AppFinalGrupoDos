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
    public partial class Usuario : ContentPage
    {
        private readonly IGoogleManager _googleManager;
        GoogleUser GoogleUser = new GoogleUser();
        public bool IsLogedIn { get; set; }

        public Usuario(string Name, string Email, Uri Picture)
        {
            _googleManager = DependencyService.Get<IGoogleManager>();

            InitializeComponent();

            lblName.Text = Name;
            lblEmail.Text = Email;
            imgProfile.Source = Picture;
        }

        private void GoogleLogout()
        {
            _googleManager.Logout();
            IsLogedIn = false;
        }

        private async void btnContinuar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Menu());
            }
            catch(Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            try
            {
                _googleManager.Logout();

                lblName.Text = "Nombre";
                lblEmail.Text = "Email";
                imgProfile.Source = "";

                await Navigation.PushAsync(new Login());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje de alerta", ex.Message, "OK");
            }
        }
    }
}