using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class BookmarksPage : ContentPage
{
	public BookmarksPage()
	{
		InitializeComponent();
		GetPropertiesList();
	}

    private async void GetPropertiesList()
    {
	 	var properties = await ApiService.GetBookmarkList();
		CvProperties.ItemsSource = properties;
    }

    void CvProperties_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as BookmarkList;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.PropertyId));
        ((CollectionView)sender).SelectedItem = null;
    }
}
