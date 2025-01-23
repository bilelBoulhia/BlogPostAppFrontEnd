using articleapp.Models;


namespace articleapp.Interfaces
{
    public interface IUser
    {
         Task<TokenModel> PerformLogin(LoginModel loginModel);
         Task<string> PerformSignUp(UserModel user);
     
         Task<string> AddHobbies(HobbiesModel hobby);

          Task<List<HobbyModel>> GetHobbies();
         Task PerformLogout();
    }
}
