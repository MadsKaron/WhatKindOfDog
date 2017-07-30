using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Tabs
{
	public partial class CustomVision : ContentPage
	{
		public CustomVision()
		{
			InitializeComponent();
			DetailButton.Opacity = 0;

		}

		private async void loadCamera(object sender, EventArgs e)
		{
			await CrossMedia.Current.Initialize(); 

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await DisplayAlert("No Camera", ":( No camera availabe.", "OK");
				return;
			}

			MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
			{
				PhotoSize = PhotoSize.Small,
				Directory = "Sample",
				Name = $"{DateTime.UtcNow}.jpg"
			});

			if (file == null)
				return;

			image.Source = ImageSource.FromStream(() =>
			  {
				  return file.GetStream();
			  });

			await MakePredictionRequest(file);




		}

		static byte[] GetImageAsByteArray(MediaFile file)
		{
			var stream = file.GetStream();
			BinaryReader binaryReader = new BinaryReader(stream);
			return binaryReader.ReadBytes((int)stream.Length); 
		}

		async Task MakePredictionRequest(MediaFile file)
		{
			var client = new HttpClient();

			client.DefaultRequestHeaders.Add("Prediction-Key", "c33e9a231f0947e9ad8327f1294b6a73");

			string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/34b9c446-de66-4522-9e92-4194dd512cb0/image?iterationId=e035a2e7-0ab4-4273-9c58-77ab0f574be3";

			HttpResponseMessage response;

			byte[] byteData = GetImageAsByteArray(file);

			using (var content = new ByteArrayContent(byteData))
			{
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
				response = await client.PostAsync(url, content);

				if (response.IsSuccessStatusCode)
				{
					var responseString = await response.Content.ReadAsStringAsync();

					EvaluationModel responseModel = JsonConvert.DeserializeObject<EvaluationModel>(responseString);
					double max = responseModel.Predictions.First().Probability;
					var breed = responseModel.Predictions.First();
					TagLabel.Text = (max >= 0.5) ? breed.Tag : "Hmmmm... Sorry cannot recognize your furry friend's breed ;)";

					//Maybe instead of label, make alert..
				}

				DetailButton.Opacity = 1; 

				file.Dispose();
			}
		}

		private void DetailButton_Clicked(object sender, EventArgs e)
		{
			Application.Current.MainPage = new AzureTable();  
		}
				
	}
}
