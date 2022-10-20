using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

    void ImgBackBtn_Clicked(System.Object sender, System.EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    async void SbProperty_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (e.NewTextValue == null) return;
        var propertiesResult = await ApiService.FindProperties(e.NewTextValue);
        CvSearch.ItemsSource = propertiesResult;
    }

    void CvSearch_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as SearchProperty;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}
