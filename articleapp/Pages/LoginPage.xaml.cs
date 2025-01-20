
using articleapp.Models;
using articleapp.Repo;
using articleapp.ViewModels;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics;

namespace articleapp;

public partial class LoginPage : ContentPage
{
	
	public LoginPage(LoginViewModel userViewModel)
	{
		InitializeComponent();
        BindingContext = userViewModel;
     }

	   
}