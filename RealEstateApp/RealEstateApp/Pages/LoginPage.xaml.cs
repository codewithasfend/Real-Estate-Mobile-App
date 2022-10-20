using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    async void BtnLogin_Clicked(System.Object sender, System.EventArgs e)
    {
       var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
       if(response)
       {
            Application.Current.MainPage = new CustomTabbedPage(); 
       }
        else
        {
            await DisplayAlert("", "Oops something went wrong.", "Cancel");
        }
    }

    async void TapJoinNow_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}
