using articleapp.Pages;

namespace articleapp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("DetailedArticle", typeof(DetailedArticleP));
            Routing.RegisterRoute("SignUp", typeof(SignupPage));
            Routing.RegisterRoute("Main", typeof(MainArticlePage));
            Routing.RegisterRoute("AddPageArticle", typeof(AddPageArticel));

            Routing.RegisterRoute("finishSignUp", typeof(FinishSignUpPage));
            Routing.RegisterRoute("Profile", typeof(ProfilePage));
            Routing.RegisterRoute("Login", typeof(LoginPage));
        }
    }
}
