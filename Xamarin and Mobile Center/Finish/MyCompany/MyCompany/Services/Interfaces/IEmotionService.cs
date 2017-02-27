using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace MyCompany
{
	public interface IEmotionService
	{
		Task<Emotion[]> GetHappinessAsync(string avatar);

		Task<float> GetAverageHappinessScoreAsync(string avatar);

		string GetHappinessMessage(float score);
	}
}
