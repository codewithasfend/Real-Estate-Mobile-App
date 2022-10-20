using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

   async void BtnRegister_Clicked(System.Object sender, System.EventArgs e)
    {
	    var response = await ApiService.RegisterUser(EntFullName.Text,EntEmail.Text,EntPassword.Text,EntPhone.Text);
		if(response)
		{
			await DisplayAlert("", "Your account has been created", "Alright");
			await Navigation.PushModalAsync(new LoginPage());
		}
		else
		{
			await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }

    async void TapLogin_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }
}
