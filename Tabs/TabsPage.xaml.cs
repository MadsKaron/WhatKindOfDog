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

		async void OnButtonClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;
			await DisplayAlert("Clicked! ", "The button labeled'" + button.Text + "' has been clicked", "OK");
		}
    }
}
