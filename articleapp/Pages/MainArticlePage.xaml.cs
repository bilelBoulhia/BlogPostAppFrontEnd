using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class MainArticlePage : ContentPage
{
	public MainArticlePage(MainPageViewModel mainPageView)
	{
		InitializeComponent();
		BindingContext  = mainPageView;
	}
}