using SpendNote.Interfaces;
using SpendNote.Models;

namespace SpendNote.Pages
{
    public class MainPage : ContentPage
    {
        bool isDarkMode = false;

        public Entry UsernameInputField = new() { Placeholder = "Имя пользователя", WidthRequest = 300 };
        public Entry PasswordInputField = new() { IsPassword = true, Placeholder = "Пароль", WidthRequest = 260 };
        public HorizontalStackLayout horizontal;
        public ImageButton ShowPasswordButton;
        public Button ForgetPassword = new() { Text = "Забыли пароль?", BackgroundColor = Colors.Transparent};
        public Button HaveAccounnt = new() { Text = "Нет аккаунта", BackgroundColor = Colors.Transparent};
        public Button SignInButton = new() { Text = "Войти", TextColor = Colors.White};

        private readonly IScreenshotProtectionService _screenService;
        public MainPage(IScreenshotProtectionService screenshotProtect)
        {
            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;

            ForgetPassword.TextColor = isDarkMode ? Colors.Black : Colors.White;

            ShowPasswordButton = new ImageButton { Source = isDarkMode ? "eyes_open_white.png" : "eyes_open.png", WidthRequest = 24, HeightRequest = 24, BackgroundColor = Colors.Transparent };

            horizontal = new HorizontalStackLayout { Children = { PasswordInputField, ShowPasswordButton } };

            ShowPasswordButton.Clicked += async (_, _) =>  await PasswordShow();

            _screenService = screenshotProtect;

            var navGUI = new HorizontalStackLayout
            {
                Children = { ForgetPassword, HaveAccounnt }
            };


            var mainPanel = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = { new Label { Text = "Вход в аккаунт", FontAttributes = FontAttributes.Bold, FontSize = 20, TextColor = isDarkMode ? Colors.White : Colors.Black, HorizontalTextAlignment = TextAlignment.Center}, UsernameInputField, horizontal, navGUI, SignInButton }
            };

            UsernameInputField.Completed += (_, _) => PasswordInputField.Focus();
            PasswordInputField.Completed += async (_, _) => await SignIn();

            Content = mainPanel;
        }

        private async Task PasswordShow()
        {
            PasswordInputField.IsPassword = !PasswordInputField.IsPassword;
            ShowPasswordButton.Source = PasswordInputField.IsPassword ? isDarkMode ? "eyes_open_white.png" : "eyes_open.png" : isDarkMode ? "eyes_close_white" : "eyes_close"; 
        }


        private async Task SignIn()
        {
            try
            {
                var firebaseService = new FirebaseService();

                var user = await firebaseService.LoginUser(UsernameInputField.Text, PasswordInputField.Text);
                if (user != null)
                {
                    await DisplayAlert("Успешно", $"Вы успешно зашли в аккаунт: {user.Username}", "Ok");
                }
                else
                {
                    await DisplayAlert("Ошибка", "Такого пользователя не существует", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка: {ex.InnerException?.Message ?? ex.Message}", "Ok");
            }
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