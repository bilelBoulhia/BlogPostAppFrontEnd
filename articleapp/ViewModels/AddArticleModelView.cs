using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using articleapp.Models;
using articleapp.Repo;

namespace articleapp.ViewModels
{
    public partial class AddArticleModelView : ObservableObject
    {
        private readonly ArticleRepo _articleRepo;

        [ObservableProperty]
        private ObservableCollection<CategoryModel> _categories;

        [ObservableProperty]
        private string _articleTitle;

        [ObservableProperty]
        private string _articleContent;

        [ObservableProperty]
        private CategoryModel _selectedCategory;

        public AddArticleModelView(ArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var categoriesList = await GetAllCategories();
            Categories = new ObservableCollection<CategoryModel>(categoriesList);
        }

        private async Task<List<CategoryModel>> GetAllCategories()
        {
            return await _articleRepo.GetAllCategory();
        }

        [RelayCommand]
        private async Task CreateArticle()
        {
            if (string.IsNullOrWhiteSpace(ArticleTitle) ||
                string.IsNullOrWhiteSpace(ArticleContent) ||
                SelectedCategory == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }

            var article = new ArticleModel
            {
                ArticleTitle = ArticleTitle,
                ArticleContent = ArticleContent,
                CategoryId = SelectedCategory.CategoryId,
                UserId = 14
            };

            try
            {
                var result = await _articleRepo.PostArticle(article);
                if (!string.IsNullOrEmpty(result))
                {
                    await Shell.Current.DisplayAlert("Success", "Article created successfully", "OK");
            
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to create article", "OK");
            }
        }
    }
}