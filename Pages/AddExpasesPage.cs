using System;
using SpendNote.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendNote.Pages
{
    public class AddExpasesPage : ContentPage
    {
        public bool isDarkMode;
        private readonly IScreenshotProtectionService _screenService;
        public AddExpasesPage(IScreenshotProtectionService screenshotProtect)
        {
            isDarkMode = Application.Current?.RequestedTheme == AppTheme.Dark;

            _screenService = screenshotProtect;

            //var 

            var addExpenses = new Grid {
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = GridLength.Auto},
                    new ColumnDefinition {Width = GridLength.Auto}
                },
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                }
            };
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
