using articleapp.Interfaces;
using articleapp.Models;
using articleapp.Repo;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

public partial class MainPageViewModel : ObservableObject
{
    private readonly ArticleRepo _articleRepo;
    [ObservableProperty]
    private ObservableCollection<CategoryModel> _categories;  
    [ObservableProperty]
    private ObservableCollection<BasicArticleWithDetails> _articles;
    [ObservableProperty]
    private string _searchText;

    public MainPageViewModel(ArticleRepo articleRepo)
    {
        _articleRepo = articleRepo;
        LoadDataAsync();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        var categoriesList = await GetAllCategories();
        Categories = new ObservableCollection<CategoryModel>(categoriesList);
        var articlesList = await GetAllArticles();
        Articles = new ObservableCollection<BasicArticleWithDetails>(articlesList);
    }


    [RelayCommand]
    private async Task NavigateToAddArticle()
    {
        await Shell.Current.GoToAsync("AddPageArticle");
    }

    private async Task<List<CategoryModel>> GetAllCategories()
    {
        return await _articleRepo.GetAllCategory();
    }

    private async Task<List<BasicArticleWithDetails>> GetAllArticles()
    {
        return await _articleRepo.GetAllArticles();
    }

   
}
