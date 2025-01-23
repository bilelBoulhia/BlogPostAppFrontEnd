using articleapp.auth;
using articleapp.Repo;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace articleapp.ViewModels
{
    public class LandingPageViewModel : ObservableObject
    {
        private readonly UserRepo _userRepo;
        private readonly AuthContext _authContext = AuthContext.Instance;

        public ICommand CreateAccountCommand { get; }
        public ICommand SignInCommand { get; }


        public bool IsAuth { get; private set; }

   
        public LandingPageViewModel(UserRepo userRepo)
        {
            _userRepo = userRepo;
           
            CreateAccountCommand = new Command(async () => await NavigateToSignUp());
            SignInCommand = new Command(async () => await NavigateToLogin());
        }

      
        public async Task CheckTokenAndNavigate()
        {
            var token = await _authContext.GetAsync("access_token");

            if (await CheckToken(token))
            {

                await Shell.Current.GoToAsync("Main");
            }
            
        }
        private async Task NavigateToSignUp()
        {
        
            await Shell.Current.GoToAsync("SignUp");
        }

        private async Task NavigateToLogin()
        {
          
            await Shell.Current.GoToAsync("Login");
        }

        private async Task<bool> CheckToken(string token)
        {
       
            return await _userRepo.CheckAuth(token);
        }
    }
}
