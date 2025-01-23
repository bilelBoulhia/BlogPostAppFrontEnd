

using articleapp.ViewModels;

namespace articleapp
{
    public partial class LandingPage : ContentPage
    {

    
        public LandingPage(LandingPageViewModel landingPageViewModel)
        {
            InitializeComponent();
            BindingContext = landingPageViewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            
            return true;
        }


    }
}
