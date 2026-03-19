using SpendNote.Interfaces;

namespace SpendNote.Pages
{
    public class MainPage : ContentPage
    {
        bool isDarkMode = false;

        public Entry UsernameInputField = new() { Placeholder = "Имя пользователя" };
        public Entry PasswordInputField = new() {IsPassword = true, Placeholder = "Пароль" };
        public Button SignIn = new() { Text = "Войти" };

        private readonly IScreenshotProtectionService _screenService;
        public MainPage(IScreenshotProtectionService screenshotProtect)
        {
            _screenService = screenshotProtect;

            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;



            var mainPanel = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label {
                        Text = "Вход в аккаунт",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20,
                        TextColor = isDarkMode ? Colors.White : Colors.Black
                    },
                    UsernameInputField,
                    PasswordInputField,
                    SignIn
                }
            };
            Content = mainPanel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _screenService?.Enable();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _screenService?.Disable();
        }
    }
}