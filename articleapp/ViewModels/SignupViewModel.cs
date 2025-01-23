using articleapp.auth;
using articleapp.Repo;
using articleapp.Models;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Alerts;
using System.ComponentModel;
using CommunityToolkit.Maui.Core;
using System.Linq;


namespace articleapp.ViewModels
{
    public partial class SignupViewModel : INotifyPropertyChanged
    {

        private readonly AuthContext _authContext = AuthContext.Instance;
        private readonly UserRepo _userRepo;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _userName = string.Empty;
        private string _userFamilyName = string.Empty;
      
        private int _userPhoneNumber;
        private DateTime _userBirthDay = DateTime.Today;
        private string _userEmail = string.Empty;
        private string _password = string.Empty;
        private string _userNameError = string.Empty;
        private string _userFamilyNameError = string.Empty;
        private string _userEmailError = string.Empty;
        private string _passwordError = string.Empty;
        private string _userPhoneNumberError = string.Empty;
        private string _userBirthDayError = string.Empty;

        public ICommand SignUpCommand { get; }
        public ICommand NavigateToSignInCommand { get; }

        public SignupViewModel( UserRepo userRepo)
        {
            
            _userRepo = userRepo;
            SignUpCommand = new AsyncRelayCommand(CreateAccountAsync);
            NavigateToSignInCommand = new AsyncRelayCommand(NavigateToSignInAsync);

        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string UserFamilyName
        {
            get => _userFamilyName;
            set
            {
                if (_userFamilyName != value)
                {
                    _userFamilyName = value;
                    OnPropertyChanged(nameof(UserFamilyName));
                }
            }
        }



        public int UserPhoneNumber
        {
            get => _userPhoneNumber;
            set
            {
                if (_userPhoneNumber != value)
                {
                    _userPhoneNumber = value;
                    OnPropertyChanged(nameof(UserPhoneNumber));
                }
            }
        }

        public DateTime UserBirthDay
        {
            get => _userBirthDay;
            set
            {
                if (_userBirthDay != value)
                {
                    _userBirthDay = value;
                    OnPropertyChanged(nameof(UserBirthDay));
                }
            }
        }

        public string UserEmail
        {
            get => _userEmail;
            set
            {
                if (_userEmail != value)
                {
                    _userEmail = value;
                    OnPropertyChanged(nameof(UserEmail));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }


        public string UserNameError
        {
            get => _userNameError;
            set
            {
                if (_userNameError != value)
                {
                    _userNameError = value;
                    OnPropertyChanged(nameof(UserNameError));
                }
            }
        }

        public string UserFamilyNameError
        {
            get => _userFamilyNameError;
            set
            {
                if (_userFamilyNameError != value)
                {
                    _userFamilyNameError = value;
                    OnPropertyChanged(nameof(UserFamilyNameError));
                }
            }
        }

        public string UserEmailError
        {
            get => _userEmailError;
            set
            {
                if (_userEmailError != value)
                {
                    _userEmailError = value;
                    OnPropertyChanged(nameof(UserEmailError));
                }
            }
        }

        public string PasswordError
        {
            get => _passwordError;
            set
            {
                if (_passwordError != value)
                {
                    _passwordError = value;
                    OnPropertyChanged(nameof(PasswordError));
                }
            }
        }

        public string UserPhoneNumberError
        {
            get => _userPhoneNumberError;
            set
            {
                if (_userPhoneNumberError != value)
                {
                    _userPhoneNumberError = value;
                    OnPropertyChanged(nameof(UserPhoneNumberError));
                }
            }
        }

        public string UserBirthDayError
        {
            get => _userBirthDayError;
            set
            {
                if (_userBirthDayError != value)
                {
                    _userBirthDayError = value;
                    OnPropertyChanged(nameof(UserBirthDayError));
                }
            }
        }

        private async Task CreateAccountAsync()
        {
            if (ValidateInput())
            {

                try
                {

                    var userModel = new UserModel
                    {
                        UserName = UserName,
                        UserFamilyName = UserFamilyName,
                        UserEmail = UserEmail,
                        UserPhoneNumber = UserPhoneNumber,
                        UserBirthDay = UserBirthDay,
                        UserHash = Password
                    };

                    var result = await _userRepo.PerformSignUp(userModel);

                    var toast = Toast.Make("Accounted created", ToastDuration.Short, 16);
                    await toast.Show();
                    var loginInfo = new LoginModel
                    {
                        UserEmail = userModel.UserEmail,
                        UserHash = userModel.UserHash,
                    };
                    var Loginresult = await _userRepo.PerformLogin(loginInfo);
                    if (Loginresult != null)
                    {

                        await _authContext.SetAsync("access_token", Loginresult.token);
                        await _authContext.SetAsync("userId", Loginresult.userId.ToString());
                        await _authContext.SetAsync("refresh_token", Loginresult.refreshToken);


                        await NavigateToFinishAsync();
                    }



                }
                catch (Exception ex)
                {

                    var toast = Toast.Make(ex.Message);
                    await toast.Show();
                }
            }
        }

        private async Task NavigateToSignInAsync()
        {
            await Shell.Current.GoToAsync("Login");
        }
        private async Task NavigateToFinishAsync()
        {
            await Shell.Current.GoToAsync("finishSignUp");
        }

        private bool ValidateInput()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserNameError = "First name is required";
                isValid = false;
            }
            else
            {
                UserNameError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(UserFamilyName))
            {
                UserFamilyNameError = "Last name is required";
                isValid = false;
            }
            else
            {
                UserFamilyNameError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(UserEmail) || !Regex.IsMatch(UserEmail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                UserEmailError = "Invalid email address";
                isValid = false;
            }
            else
            {
                UserEmailError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                PasswordError = "Password must be at least 6 characters long";
                isValid = false;
            }
            else
            {
                PasswordError = string.Empty;
            }

            if (UserPhoneNumber <= 0 || !Regex.IsMatch(UserPhoneNumber.ToString(), @"^(5|6|7)[0-9]{8}$"))
            {
                UserPhoneNumberError = "Invalid phone number";
                isValid = false;
            }
            else
            {
                UserPhoneNumberError = string.Empty;
            }

            if (UserBirthDay >= DateTime.Today)
            {
                UserBirthDayError = "Invalid birth date";
                isValid = false;
            }
            else
            {
                UserBirthDayError = string.Empty;
            }

            return isValid;
        }
        public void ResetForm()
        {
            UserName = string.Empty;
            UserFamilyName = string.Empty;
            UserEmail = string.Empty;
            Password = string.Empty;
            UserPhoneNumber = 0;
            UserBirthDay = DateTime.Today;

        
            UserNameError = string.Empty;
            UserFamilyNameError = string.Empty;
            UserEmailError = string.Empty;
            PasswordError = string.Empty;
            UserPhoneNumberError = string.Empty;
            UserBirthDayError = string.Empty;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
