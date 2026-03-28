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
            var mainLayout = new Grid();

            var avatar = new ImageButton
            {
                HeightRequest = 30,
                WidthRequest = 30,
                Margin = new Thickness(2),
                Source = "default_avatar.jpg"
            };

            var profileButton = new Button
            {
                Text = $"{Session.SessionName}",
                BackgroundColor = Colors.Transparent,
                FontAttributes = FontAttributes.Bold
            };

            var horizontalUpper = new HorizontalStackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children = {avatar, profileButton}
            };

            profileButton.TextColor = isDarkMode ? Colors.White : Colors.Black;


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
            


            _screenService = screenshotProtection;
            mainLayout.Children.Add(createExpenses);
            mainLayout.Children.Add(horizontalUpper);
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
