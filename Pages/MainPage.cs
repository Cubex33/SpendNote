using SpendNote.Interfaces;
using SpendNote.Models;

namespace SpendNote.Pages
{
    public class MainPage : ContentPage
    {
        bool isDarkMode = false;

        public Entry UsernameInputField = new() { Placeholder = "Имя пользователя", WidthRequest = 300 };
        public Entry PasswordInputField = new() { IsPassword = true, Placeholder = "Пароль", WidthRequest = 260 };
        public Entry RegUsernameInputField = new() { Placeholder = "Имя пользователя", WidthRequest = 300 };
        public Entry RegPassword = new() { IsPassword = true, Placeholder = "Пароль", WidthRequest = 260 };
        public Entry RegConfirmPassword = new() { IsPassword = true,
        Placeholder = "Повторный пароль", WidthRequest = 260 };

        private readonly IScreenshotProtectionService _screenService;
        public MainPage(IScreenshotProtectionService screenshotProtect)
        {
            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;

            var HaventAccount = new Button { Text = "Нет аккаунта", BackgroundColor = Colors.Transparent };

            var SignInButton = new Button { Text = "Войти", TextColor = Colors.White };

            var LoginInButton = new Button { Text = "Создать аккаунта", TextColor = Colors.White };

            HaventAccount.TextColor = isDarkMode ? Colors.White : Colors.Black;

            var HaveAccount = new Button { Text = "Есть аккаунта", BackgroundColor = Colors.Transparent };

            HaveAccount.TextColor = isDarkMode ? Colors.White : Colors.Black;

            var ForgetPassword = new Button { Text = "Забыли пароль?", BackgroundColor = Colors.Transparent };

            ForgetPassword.TextColor = isDarkMode ? Colors.White : Colors.Black;

            var ShowPasswordButton = new ImageButton { Source = isDarkMode ? "eyes_open_white.png" : "eyes_open.png", WidthRequest = 24, HeightRequest = 24, BackgroundColor = Colors.Transparent };
            var RegShowPasswordButton = new ImageButton { Source = isDarkMode ? "eyes_open_white.png" : "eyes_open.png", WidthRequest = 24, HeightRequest = 24, BackgroundColor = Colors.Transparent };
            var RegShowConfirmPasswordButton = new ImageButton { Source = isDarkMode ? "eyes_open_white.png" : "eyes_open.png", WidthRequest = 24, HeightRequest = 24, BackgroundColor = Colors.Transparent };

            var horizontal = new HorizontalStackLayout { Children = { PasswordInputField, ShowPasswordButton } };
            var regPasswordHorizontal = new HorizontalStackLayout { Children = { RegPassword, RegShowPasswordButton } };
            var regConfirmPasswordHorizontal = new HorizontalStackLayout { Children = { RegConfirmPassword, RegShowConfirmPasswordButton } };


            ShowPasswordButton.Clicked += async (_, _) => { PasswordInputField.IsPassword = !PasswordInputField.IsPassword; ShowPasswordButton.Source = PasswordInputField.IsPassword ? isDarkMode ? "eyes_open_white.png" : "eyes_open.png" : isDarkMode ? "eyes_close_white" : "eyes_close"; };
            RegShowPasswordButton.Clicked += async (_, _) => { RegPassword.IsPassword = !RegPassword.IsPassword; RegShowPasswordButton.Source = RegPassword.IsPassword ? isDarkMode ? "eyes_open_white.png" : "eyes_open.png" : isDarkMode ? "eyes_close_white" : "eyes_close"; };
            RegShowConfirmPasswordButton.Clicked += async (_, _) => { RegConfirmPassword.IsPassword = !RegConfirmPassword.IsPassword; RegShowConfirmPasswordButton.Source = RegConfirmPassword.IsPassword ? isDarkMode ? "eyes_open_white.png" : "eyes_open.png" : isDarkMode ? "eyes_close_white" : "eyes_close"; };

            _screenService = screenshotProtect;

            var navGUI = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = { ForgetPassword, HaventAccount }
            };

            var registretionNavGUI = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = { HaveAccount }
            };


            var mainPanel = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = { new Label { Text = "Вход в аккаунт", FontAttributes = FontAttributes.Bold, FontSize = 20, TextColor = isDarkMode ? Colors.White : Colors.Black, HorizontalTextAlignment = TextAlignment.Center }, UsernameInputField, horizontal, navGUI, SignInButton }
            };

            var registritionPanel = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children = { new Label { Text = "Создание аккаунта", FontAttributes = FontAttributes.Bold, FontSize = 20, TextColor = isDarkMode ? Colors.White : Colors.Black, HorizontalTextAlignment = TextAlignment.Center }, RegUsernameInputField, regPasswordHorizontal, regConfirmPasswordHorizontal, registretionNavGUI, LoginInButton }
            };


            HaveAccount.Clicked += async (_, _) => Content = mainPanel;
            HaventAccount.Clicked += async (_, _) => Content = registritionPanel;
            SignInButton.Clicked += async (_, _) => await SignIn();
            LoginInButton.Clicked += async (_, _) => await Register();

            UsernameInputField.Completed += (_, _) => PasswordInputField.Focus();
            PasswordInputField.Completed += async (_, _) => await SignIn();
            RegUsernameInputField.Completed += (_, _) => RegPassword.Focus();
            RegPassword.Completed += (_, _) => RegConfirmPassword.Focus();
            RegConfirmPassword.Completed += async (_, _) => await Register();

            Content = mainPanel;
        }

        private async Task SignIn()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(UsernameInputField.Text) || string.IsNullOrWhiteSpace(PasswordInputField.Text))
                {
                    await DisplayAlert("Ошибка", $"Есть пустые поля", "Ok");
                    return;
                }
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

        private async Task Register()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RegUsernameInputField.Text) || string.IsNullOrWhiteSpace(RegPassword.Text) || string.IsNullOrWhiteSpace(RegConfirmPassword.Text))
                {
                    await DisplayAlert("Ошибка", $"Есть пустые поля", "Ok");
                    return;
                }

                if (RegPassword.Text != RegConfirmPassword.Text)
                {
                    await DisplayAlert("Ошибка", $"Пароль не совпадает", "Ok");
                    return;
                }

                var firebaseService = new FirebaseService();
                var User = new Users
                {
                    Username = RegUsernameInputField.Text,
                    Password = RegPassword.Text,
                    CreatedAt = DateTime.Now.ToString("HH:mm:HH dd.MM.yyyy")
                };
                await firebaseService.RegisterUser(User);
                await DisplayAlert("Успешно", $"Аккаунт создан", "Ok");
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