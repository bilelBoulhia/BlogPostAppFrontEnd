using articleapp.Models;
using articleapp.Repo;
using articleapp.ViewModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics;
namespace articleapp.Pages;

public partial class ProfilePage : ContentPage
{
	ProfileViewModel _viewModel;
    public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}