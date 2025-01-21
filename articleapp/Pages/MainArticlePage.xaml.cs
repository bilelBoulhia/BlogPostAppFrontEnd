using articleapp.Interfaces;
using articleapp.Models;
using articleapp.Repo;
using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class MainArticlePage : ContentPage
{
	public MainArticlePage(MainPageViewModel mainPageView)
	{
		InitializeComponent();
		BindingContext  = mainPageView;
	}

  
    private async void OnEntryFocused(object sender, FocusEventArgs e)
    {
    
        await Navigation.PushModalAsync(new SearchModal());
    }

    private void checkAuth() { 
    
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