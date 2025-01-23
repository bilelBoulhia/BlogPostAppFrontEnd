using articleapp.auth;
using articleapp.Models;
using articleapp.Repo;
using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class OtherPoeplePage : ContentPage
{
 
	public OtherPoeplePage(object userId)
	{
		InitializeComponent();
		BindingContext = new OtherPoepleViewModel(userId,new UserRepo(),new ArticleRepo());																																															
	}

    private async void OnArticleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is BasicArticleWithDetails selectedArticle)
        {

            await Navigation.PushAsync(new DetailedArticleP(selectedArticle.ArticleId));


            ((CollectionView)sender).SelectedItem = null;
        }
    }
}