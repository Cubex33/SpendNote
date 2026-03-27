using SpendNote.Interfaces;

namespace SpendNote.Pages
{
    public class ExpensesPage : ContentPage
    {
        private readonly IScreenshotProtectionService _screenService;
        public ExpensesPage(IScreenshotProtectionService screenshotProtection) {
            NavigationPage.SetHasNavigationBar(this, false);
            var mainLayout = new Grid();
            
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
