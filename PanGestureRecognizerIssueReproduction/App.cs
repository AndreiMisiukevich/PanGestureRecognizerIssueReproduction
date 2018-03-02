using System;

using Xamarin.Forms;
using System.Linq;

namespace PanGestureRecognizerIssueReproduction
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainPage());
        }
    }

    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var first = new Button { Text = "Scroller" };
            first.Clicked += (sender, e) => Navigation.PushAsync(new FirstPage());

            var second = new Button { Text = "TapGesture" };
            second.Clicked += (sender, e) => Navigation.PushAsync(new SecondPage());

            Content = new StackLayout
            {
                Children = {
                    first,
                    second
                }
            };
        }
    }

    public class FirstPage : ContentPage
    {
        public FirstPage()
        {
            var longItem = new Label
            {
                Text = new string(Enumerable.Range(0, 255).Select(x => (char)x).ToArray()),
                FontSize = 60
            };

            var scroller = new ScrollView
            {
                Content = longItem
            };

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (sender, e) => {
                DisplayAlert("I'm working!", "I'm working!", "I'm working!"); //Not calling :(
            };

            Content = new ContentView
            {
                Content = scroller,
                GestureRecognizers = {
                    panGesture
                }
            };
        }
    }

    public class SecondPage : ContentPage
    {
        public SecondPage()
        {
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (sender, e) => {
                DisplayAlert("TAP", "TAP", "TAP");
            };

            var tapItem = new ContentView
            {
                BackgroundColor = Color.Gold,
                GestureRecognizers = {
                    tapGesture
                }
            };

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += (sender, e) => {
                DisplayAlert("I'm working!", "I'm working!", "I'm working!"); //Not calling :(
            };

            Content = new ContentView
            {
                Content = tapItem,
                GestureRecognizers = {
                    panGesture
                }
            };
        }
    }
}
