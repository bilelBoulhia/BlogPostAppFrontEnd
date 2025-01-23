using articleapp.ViewModels;

namespace articleapp;

public partial class FinishSignUpPage : ContentPage
{
	public FinishSignUpPage(FinsishSignUpViewModel finishSignUpViewModel)
	{
		InitializeComponent();
        BindingContext = finishSignUpViewModel;
	}
    
}