using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MyCompany.Models;
using Xamarin.Forms;

namespace MyCompany.ViewModels
{
	public class EmployeeDetailViewModel : ViewModelBase
	{
		public EmployeeDetailViewModel(Employee selectedEmployee = null)
		{
			SelectedEmployee = selectedEmployee;
		}

		public EmployeeDetailViewModel()
		{

		}

		public async Task<string> GetEmotion()
		{
			if (IsBusy)
				return null;

			Exception error = null;
			try
			{
				IsBusy = true;

				IEmotionService emotionService = new EmotionService();

				var score = await emotionService.GetAverageHappinessScoreAsync(SelectedEmployee.Avatar);

				return emotionService.GetHappinessMessage(score);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error: " + ex);
				error = ex;
			}
			finally
			{
				IsBusy = false;
			}

			if (error != null)
				await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");

			return null;
		
		}

		Employee _selectedEmployee;
		public Employee SelectedEmployee
		{
			get { return _selectedEmployee; }
			set { SetProperty(ref _selectedEmployee, value); }
		}
	}
}
