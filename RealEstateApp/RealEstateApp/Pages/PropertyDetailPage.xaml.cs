using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
	private string phoneNumber;
	private bool IsBookmarkEnabled;
	private int propertyId;
	private int bookmarkId;
    public PropertyDetailPage(int propertyId)
	{
		InitializeComponent();
		GetPropertyDetail(propertyId);
		this.propertyId = propertyId;
	}

    private async void GetPropertyDetail(int propertyId)
    {
		var property = await ApiService.GetPropertyDetail(propertyId);
		LblPrice.Text = "$ "+ property.Price;
		LblDescription.Text = property.Detail;
		LblAddress.Text = property.Address;
		ImgProperty.Source = property.FullImageUrl;
		phoneNumber = property.Phone;

		if(property.Bookmark == null)
		{
			ImgbookmarkBtn.Source = "bookmark_empty_icon";
			IsBookmarkEnabled = false;
		}
		else
		{
			ImgbookmarkBtn.Source = property.Bookmark.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
			bookmarkId = property.Bookmark.Id;
			IsBookmarkEnabled = true;
		}

    }

    void TapMessage_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
		var message = new SmsMessage("Hi! I am interested in your property", phoneNumber);
		Sms.ComposeAsync(message);
    }

    void TapCall_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
		PhoneDialer.Open(phoneNumber);
    }

    void ImgbackBtn_Clicked(System.Object sender, System.EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    async void ImgbookmarkBtn_Clicked(System.Object sender, System.EventArgs e)
    {
		if(IsBookmarkEnabled == false)
		{
			// Add a bookmark
			var addBookmark = new AddBookmark()
			{
				User_Id = Preferences.Get("userid",0),
				PropertyId = propertyId
			};
			var response = await ApiService.AddBookmark(addBookmark);
			if (response)
			{
				ImgbookmarkBtn.Source = "bookmark_fill_icon";
				IsBookmarkEnabled = true;
			}
		}
		else
		{
			// Delete a bookmark
			var response = await ApiService.DeleteBookmark(bookmarkId);
			if (response)
			{
				ImgbookmarkBtn.Source = "bookmark_empty_icon";
				IsBookmarkEnabled = false;
			}
		}
    }
}
