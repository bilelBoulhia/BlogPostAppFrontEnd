using articleapp.auth;
using articleapp.Repo;
using articleapp.ViewModels;

namespace articleapp.Pages
{
    public partial class DetailedArticleP : ContentPage
    {

         private object articleId;
        public DetailedArticleP(object articleId)
        {
            InitializeComponent();
            this.articleId = articleId;
            BindingContext = new DetailedArticeleViewModel(articleId, new ArticleRepo());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new DetailedArticeleViewModel(articleId, new ArticleRepo());
        }

    }
}
