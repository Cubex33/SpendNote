using SpendNote.Interfaces;

namespace SpendNote.Pages
{
    public class ExpensesPage : ContentPage
    {
        bool isDarkMode = false;
        private readonly IScreenshotProtectionService _screenService;
        public ExpensesPage(IScreenshotProtectionService screenshotProtection) {
            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;
            NavigationPage.SetHasNavigationBar(this, false);
            var mainLayout = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Star },  // основной контент
                    new RowDefinition { Height = GridLength.Auto },  // нижняя панель
                }
            };

            var createExpenses = new Button
            {
                Text = "+",
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.End,
                Margin = 20,
                CornerRadius = 30,
                WidthRequest = 60,
                HeightRequest = 60,
                FontSize = 30
            };

            var mainPageButton = new ImageButton
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Source = isDarkMode ? "home_page_button_white.png" : "home_page_button.png",
            };

            var searchPageButton = new ImageButton
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Source = isDarkMode ? "search_page_button_white.png" : "search_page_button.png",
            };

            var accountPageButton = new ImageButton
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Source = isDarkMode ? "account_page_button_white.png" : "account_page_button.png",
            };

            var settingsPageButton = new ImageButton
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Source = isDarkMode ? "settings_page_button_white.png" : "settings_page_button.png",
            };

            var lowerPanel = new Grid {
                VerticalOptions = LayoutOptions.End,
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

            _screenService = screenshotProtection;

            Grid.SetRow(createExpenses, 0);
            Grid.SetRow(lowerPanel, 1);

            Grid.SetColumn(mainPageButton, 0);
            Grid.SetColumn(searchPageButton, 1);
            Grid.SetColumn(accountPageButton, 2);
            Grid.SetColumn(settingsPageButton, 3);

            mainLayout.Children.Add(createExpenses);
            mainLayout.Children.Add(lowerPanel);
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
