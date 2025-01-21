using articleapp.Repo;
using articleapp.ViewModels;

namespace articleapp.Pages;

public partial class DetailedArticleP : ContentPage
{
	public DetailedArticleP(object articleId)
	{
		InitializeComponent();
        BindingContext = new DetailedArticeleViewModel(articleId, new ArticleRepo());
    }
}