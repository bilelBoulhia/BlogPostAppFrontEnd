using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class AddPageArticel : ContentPage
{
	public AddPageArticel(AddArticleModelView addArticleModelView)
	{
		InitializeComponent();
		BindingContext = addArticleModelView;
	}
}