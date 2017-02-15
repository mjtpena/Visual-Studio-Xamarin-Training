using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace MyCompany
{
	public class EmotionService : IEmotionService
	{
		public EmotionService()
		{
		}

		public async Task<Emotion[]> GetHappinessAsync(string avatar)
		{
			var emotionClient = new EmotionServiceClient(Constants.EmotionAPI);

			Emotion[] emotionResults = await emotionClient.RecognizeAsync(avatar);

			if (emotionResults == null || emotionResults.Count() == 0)
			{
				throw new Exception("Can't detect face");
			}

			return emotionResults;
		}

		// For more than 1 person in picture
		public async Task<float> GetAverageHappinessScoreAsync(string avatar)
		{
			Emotion[] emotionResults = await GetHappinessAsync(avatar);

			float score = 0;
			foreach (var emotionResult in emotionResults)
			{
				score = score + emotionResult.Scores.Happiness;
			}

			return score / emotionResults.Count();
		}

		public string GetHappinessMessage(float score)
		{
			score = score * 100;
			double result = Math.Round(score, 2);

			if (score >= 50)
				return result + " % Happy";
			else
				return result + "% Sad";
		}
	}
}
