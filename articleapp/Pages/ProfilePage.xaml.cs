using articleapp.Models;
using articleapp.Repo;
using articleapp.ViewModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics;
namespace articleapp.Pages;

public partial class ProfilePage : ContentPage
{

    public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
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