using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		LblUserName.Text = "Hi " + Preferences.Get("username", string.Empty);
		GetCategories();
		GetTrendingProperties();

	}

    private async void GetTrendingProperties()
    {
		var properties = await ApiService.GetTrendingProperties();
		CvTopPicks.ItemsSource = properties;
    }

    private async void GetCategories()
    {
	 	var categories = await ApiService.GetCategories();
		CvCategories.ItemsSource = categories;
    }

    void CvCategories_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
		var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
		if (currentSelection == null) return;
		Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
		((CollectionView)sender).SelectedItem = null;
    }

    void CvTopPicks_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }

    void TapSearch_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Navigation.PushModalAsync(new SearchPage());
    }
}
