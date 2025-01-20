using articleapp.Pages;

namespace articleapp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("SignUp", typeof(SignupPage));
            Routing.RegisterRoute("Main", typeof(MainArticlePage));
            Routing.RegisterRoute("spArticle", typeof(SpecificArticle));
            Routing.RegisterRoute("finishSignUp", typeof(FinishSignUpPage));
            Routing.RegisterRoute("Profile", typeof(ProfilePage));
            Routing.RegisterRoute("Login", typeof(LoginPage));
        }
    }
}
