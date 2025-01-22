using articleapp.Models;
using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class SearchModal : TabbedPage
{
   
    public SearchModal(SearchViewModel searchViewModel)
	{
		InitializeComponent();
		BindingContext = searchViewModel;
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