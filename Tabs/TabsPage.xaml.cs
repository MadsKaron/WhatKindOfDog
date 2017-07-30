using System;
using Xamarin.Forms;

namespace Tabs
{
    public partial class TabsPage : ContentPage
    {
        public TabsPage()
        {
            InitializeComponent();
        }

		async void developerInfoButtonClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;
			await DisplayAlert("Who is the Developer? ", "Hey, I am Jiwon. This app is for detecting dog's breed.\n Hope you enjoy it :^)" , "Cheers");
		}

		private void loadNextPage(object sender, EventArgs args)
		{
			Application.Current.MainPage = new CustomVision();
		}
    }
}
