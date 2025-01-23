
using articleapp.auth;
using articleapp.Repo;
using articleapp.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CommunityToolkit.Maui.Alerts;

using CommunityToolkit.Maui.Core;

namespace articleapp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthContext _authContext = AuthContext.Instance;
        private readonly UserRepo _userRepo;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

 

        [ObservableProperty]
        private string errorMessage;

        public LoginViewModel(UserRepo userRepo)
        {
        
            _userRepo = userRepo;
            LoginCommand = new AsyncRelayCommand(PerformLoginAsync);
        }

        public ICommand LoginCommand { get; }



        private async Task PerformLoginAsync()
        {
            try
            {
                
                ErrorMessage = string.Empty;

               
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    var toast = Toast.Make($"nsit {(Email == null ? "password" : "l'email")}", ToastDuration.Short, 16);
                    await toast.Show();
                    return; 
                }

                var loginModel = new LoginModel
                {
                    UserEmail = Email,
                    UserHash = Password
                };

                
                var result = await _userRepo.PerformLogin(loginModel);

                if (result != null)
                {

                    await _authContext.SetAsync("access_token", result.token);
                    await _authContext.SetAsync("refresh_token", result.refreshToken);
                    await _authContext.SetAsync("userId", result.userId.ToString());


                    var toast = Toast.Make("Signed in", ToastDuration.Short, 16);
                    await toast.Show();
                    await Shell.Current.GoToAsync("///Main");
                }
            }
            catch (Exception ex)
            {

                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    // Show a Toast for Android
                    var toast = Toast.Make(ex.Message);
                    await toast.Show();
                }
                else if (DeviceInfo.Platform == DevicePlatform.UWP) // Windows
                {
                    // Show a Display Alert for Windows
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }




        [RelayCommand]
        private async Task SignUpAsync()
        {
            await Shell.Current.GoToAsync("SignUp");
        }


    }
}