using System;
using SpendNote.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendNote.Pages
{
    //beta - Develop
    public class AddExpasesPage : ContentPage
    {
        public bool isDarkMode;
        private readonly IScreenshotProtectionService _screenService;
        public AddExpasesPage(IScreenshotProtectionService screenshotProtect)
        {
            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;

            _screenService = screenshotProtect;

            var logo = new ImageButton
            {
                Source = isDarkMode ? "icons_movie_white" : "icons_movie",
                WidthRequest = 100,
                HeightRequest = 100,
                BackgroundColor = Colors.Transparent
            };

            var nameText = new Entry
            {
                Placeholder = "Название",
                Margin = new Thickness(10, 30),
                MaxLength = 20
            };

            var descriptionText = new Editor
            {
                Placeholder = "Описание",
                Margin = new Thickness(20, 0)
            };

            var priceText = new Entry
            {
                Placeholder = "Цена",
                Margin = new Thickness(150, 0),
                HorizontalTextAlignment = TextAlignment.Center
            };

            var addExpenses = new Grid {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = GridLength.Star},
                    new ColumnDefinition {Width = GridLength.Star}
                },
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Star},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                }
            };

            addExpenses.Add(logo, 0, 0);
            addExpenses.Add(nameText, 1, 0);
            addExpenses.Add(descriptionText, 0, 1);
            addExpenses.SetColumnSpan(descriptionText, 2);
            addExpenses.Add(priceText, 0, 2);
            addExpenses.SetColumnSpan(priceText, 2);
            Content = addExpenses;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _screenService.Disable();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}
