using RealEstateApp.Pages;

namespace RealEstateApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

	 	var accessToken = Preferences.Get("accesstoken", string.Empty);
		if (string.IsNullOrEmpty(accessToken))
		{
            MainPage = new RegisterPage();
        }
		else
		{
			MainPage = new CustomTabbedPage();
		}
    }
}

