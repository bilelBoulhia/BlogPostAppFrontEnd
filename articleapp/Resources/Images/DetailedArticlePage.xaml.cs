
using articleapp.Repo;
using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class DetailedArticlePage : ContentPage
{
    public DetailedArticlePage(object articleId)
    {
        InitializeComponent();
        BindingContext = new DetailedArticeleViewModel(articleId, new ArticleRepo());
    }
}
