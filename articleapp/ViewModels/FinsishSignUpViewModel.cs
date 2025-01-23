using articleapp.auth;
using articleapp.Models;
using articleapp.Repo;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.Diagnostics;

namespace articleapp.ViewModels
{
    public partial class FinsishSignUpViewModel : ObservableObject
    {
  
        private readonly UserRepo _userRepo;
        private readonly AuthContext _authContext =AuthContext.Instance;

        [ObservableProperty]
        private string imageurl;

        [ObservableProperty]
        private ObservableCollection<HobbyModel> availableHobbies = new();

        [ObservableProperty]
        private ObservableCollection<HobbyModel> selectedHobbies = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool canContinue;

        public FinsishSignUpViewModel(UserRepo userRepo)
        {
            
            _userRepo = userRepo;
            LoadHobbiesAsync().ConfigureAwait(false);
  
        }

        private async Task LoadHobbiesAsync()
        {
            try
            {
                IsLoading = true;
                var response = await _userRepo.GetHobbies();

                if (response != null)
                {
                   
                AvailableHobbies.Clear();

                foreach (var hobby in  response)
                {
                   AvailableHobbies.Add(hobby);
                }
                        
                 
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading hobbies: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task SaveHobbiesAsync()
        {
           

            try
            {
                IsLoading = true;


                var userID = await _authContext.GetAsync("userId");


                var hobbiesModel = new HobbiesModel
                {
                    userId = Convert.ToInt32(userID),
                    hobbies = SelectedHobbies.Select(h => h.HobbyId).ToList()
                };

                var result = await _userRepo.AddHobbies(hobbiesModel);

                var toast = Toast.Make("hobbies added", ToastDuration.Short, 16);
                await toast.Show();
                await Shell.Current.GoToAsync("///Main");
            }
            catch (Exception ex)
            {

                var toast = Toast.Make(ex.Message);
                await toast.Show();

            }

            finally
            {
                IsLoading = false;
            }
        }






        [RelayCommand]
        private void ToggleHobby(HobbyModel hobby)
        {
            if (SelectedHobbies.Contains(hobby))
            {
                SelectedHobbies.Remove(hobby);
            }
            else if (SelectedHobbies.Count < 3)
            {
                SelectedHobbies.Add(hobby);
            }

        
            CanContinue = SelectedHobbies.Count == 3;
        }
    }
}