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
    private ObservableCollection<CategoryModel> _categories;  // Ensure it is a collection of CategoryModel
    [ObservableProperty]
    private ObservableCollection<ArticleModel> _articles;
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
        Articles = new ObservableCollection<ArticleModel>(articlesList);
    }

    private async Task<List<CategoryModel>> GetAllCategories()
    {
        return await _articleRepo.GetAllCategory();
    }

    private async Task<List<ArticleModel>> GetAllArticles()
    {
        return await _articleRepo.GetAllArticles();
    }

    [RelayCommand]
    private async Task SearchArticles()
    {

        
    }
}
