using System.Windows;
using System.Windows.Controls;

namespace WpfApp_CPTest_III
{
    /// <summary>
    /// Interaction logic for PageMainMenu.xaml
    /// </summary>
    public partial class PageMainMenu : Page
    {
        public PageMainMenu()
        {
            InitializeComponent();
        }

        private void ButtonQuitDB_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void ButtonClosedCompanies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCompaniesDatagrid(false));
        }

        private void ButtonOpenedCompanies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageCompaniesDatagrid(true));
        }
    }
}
