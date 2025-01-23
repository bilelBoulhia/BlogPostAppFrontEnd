
using articleapp.auth;
using articleapp.Interfaces;
using articleapp.Models;
using articleapp.Repo;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace articleapp.ViewModels;
public partial class MainPageViewModel : ObservableObject
{
    private readonly ArticleRepo _articleRepo;
    private readonly AuthContext _authContext = AuthContext.Instance;
    private readonly UserRepo _userRepo;

    [ObservableProperty]
    private ObservableCollection<CategoryModel> _categories;

    [ObservableProperty]
    private ObservableCollection<BasicArticleWithDetails> _articles;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private CategoryModel _selectedCategory;

    public MainPageViewModel(ArticleRepo articleRepo,  UserRepo userRepo)
    {
        _articleRepo = articleRepo;
        _userRepo = userRepo;
        CheckAuthenticationAndLoadData();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        var categoriesList = await GetAllCategories();
        Categories = new ObservableCollection<CategoryModel>(categoriesList);

  
        Categories.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "All" });

        await LoadAllArticles();
    }

    private async Task LoadAllArticles()
    {
        var articlesList = await GetAllArticles();
        Articles = new ObservableCollection<BasicArticleWithDetails>(articlesList);
    }

    [RelayCommand]
    private async Task SelectCategory(CategoryModel category)
    {
      
        if (category.CategoryId == 0 || category == SelectedCategory)
        {
            SelectedCategory = null;
            await LoadAllArticles();
            return;
        }

      
        SelectedCategory = category;

       
        var articlesList = await GetArticlesByCategory(category.CategoryId);
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

    private async Task<List<HobbyModel>> GetUserHobbies(int userid)
    {
        return await _userRepo.GetUserHobby(userid);
    }

    private async Task<List<BasicArticleWithDetails>> GetArticlesByCategory(int categoryId)
    {
        return await _articleRepo.GetArticlesByCategory(categoryId);
    }
    private async Task CheckAuthenticationAndLoadData()
    {
        var token = await _authContext.GetAsync("access_token");
        var userId = await _authContext.GetAsync("userId");

        if (string.IsNullOrEmpty(token) || !await CheckToken(token))
        {

            await Shell.Current.GoToAsync("LandingPage"); 
            return;
        }

        var userHobbies = await GetUserHobbies(Convert.ToInt32(userId));
        if ( userHobbies == null)
        {

            await Shell.Current.GoToAsync("finishSignUp");
            return;
        }

        await LoadDataAsync();
    }


    private async Task<bool> CheckToken(string token)
    {
        return await _userRepo.CheckAuth(token);
    }

    private async Task<List<BasicArticleWithDetails>> GetAllArticles()
    {
        return await _articleRepo.GetAllArticles();
    }
}