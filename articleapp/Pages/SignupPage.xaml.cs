using articleapp.ViewModels;

namespace articleapp;

public partial class SignupPage : ContentPage
{
    private readonly SignupViewModel _viewModel;
    public SignupPage(SignupViewModel SignupViewModel)
	{
		InitializeComponent();
        _viewModel = SignupViewModel;
        BindingContext = SignupViewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.ResetForm();
    }
}