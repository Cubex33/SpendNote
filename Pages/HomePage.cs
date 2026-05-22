using Microsoft.Maui.Controls.Shapes;
using SpendNote.Interfaces;

namespace SpendNote.Pages
{
    public class HomePage : ContentPage
    {
        bool isDarkMode = false;
        private readonly IScreenshotProtectionService _screenService;

        public HomePage(IScreenshotProtectionService screenshotProtection)
        {
            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;
            NavigationPage.SetHasNavigationBar(this, false);

            var verticalExpencesStack = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "Остаток:", FontSize = 25},
                    new Label { Text = $"₸ {Session.Remains}", FontSize = 30, TextColor = Colors.Black}
                }
            };

            var expenses = new Border
            {
                Margin = new Thickness(20, 0),
                Stroke = Colors.Transparent,
                StrokeThickness = 0,

                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(12)
                },

                Background = Colors.White,

                Shadow = new Shadow
                {
                    Brush = Colors.Black,
                    Offset = new Point(0, 6),
                    Radius = 15,
                    Opacity = 0.2f
                },

                Content = verticalExpencesStack
            };

            var verticalStack = new VerticalStackLayout
            {
                Spacing = 10,
                Children =
                {
                    new Label { Text = "SpendNote", FontSize = 30, TextColor = isDarkMode ? Colors.White : Colors.Black, FontAttributes = FontAttributes.Bold, Margin = new Thickness(20, 0), FontFamily="Helvetica"},
                    new Label { Text = $"Здравствуйте, {Session.SessionName}!", FontSize = 20, TextColor = isDarkMode ? Colors.White : Colors.Black, Margin = new Thickness(20, -10), FontFamily="Helvetica"},
                    expenses
                }
            };

            var scrollview = new ScrollView
            {
                Padding = 2,
                Content = verticalStack
            };

            var mainLayout = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto },
                }
            };

            var mainPageButton = new ImageButton
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Source = isDarkMode ? "home_page_button_white.png" : "home_page_button.png",
            };

            var searchPageButton = new ImageButton
            {
                WidthRequest = 35,
                HeightRequest = 35,
                Source = isDarkMode ? "search_page_button_white.png" : "search_page_button.png",
            };

            var accountPageButton = new ImageButton
            {
                WidthRequest = 50,
                HeightRequest = 50,
                Source = isDarkMode ? "account_page_button_white.png" : "account_page_button.png",
            };

            var settingsPageButton = new ImageButton
            {
                WidthRequest = 55,
                HeightRequest = 55,
                Source = isDarkMode ? "settings_page_button_white.png" : "settings_page_button.png",
            };

            var lowerPanel = new Grid
            {
                Padding = new Thickness(20, 10),
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Star },
                },
                Children = { mainPageButton, searchPageButton, accountPageButton, settingsPageButton }
            };

            mainPageButton.Clicked += (s, e) => { };

            var lowerPanelBorder = new Border
            {
                Content = lowerPanel,
                StrokeThickness = 0.4,
                Stroke = Colors.Gray,
            };

            _screenService = screenshotProtection;

            Grid.SetRow(scrollview, 0);
            Grid.SetRow(lowerPanelBorder, 1);

            Grid.SetColumn(mainPageButton, 0);
            Grid.SetColumn(searchPageButton, 1);
            Grid.SetColumn(accountPageButton, 2);
            Grid.SetColumn(settingsPageButton, 3);

            mainLayout.Children.Add(scrollview);
            mainLayout.Children.Add(lowerPanelBorder);
            Content = mainLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _screenService.Disable();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
