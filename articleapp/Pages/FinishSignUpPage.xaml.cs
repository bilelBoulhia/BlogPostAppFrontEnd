using articleapp.ViewModels;

namespace articleapp;

public partial class FinishSignUpPage : ContentPage
{
	public FinishSignUpPage(FinsishSignUpViewModel finishSignUpViewModel)
	{
		InitializeComponent();
        BindingContext = finishSignUpViewModel;
	}
    public async void TakePhoto()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }
}