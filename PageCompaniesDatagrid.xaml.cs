using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp_CPTest_III
{
    /// <summary>
    /// Interaction logic for PageCompaniesDatagrid.xaml
    /// </summary>
    public partial class PageCompaniesDatagrid : Page
    {
        // Какие компании показывать на этой странице
        private readonly bool _showOpenedCompanies;

        // Простое представление каждой колонки
        // ИНН, Наименование, Город, Область
        private class DataItem
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
            public string Column3 { get; set; }
            public string Column4 { get; set; }
        }

        public PageCompaniesDatagrid(bool showOpenedCompanies = false)
        {
            // Определиться, какие компании показать пользователю
            _showOpenedCompanies = showOpenedCompanies;

            InitializeComponent();

            // Заполнить DataGrid Данными
            PopulateDataGrid();
        }

        // Возврат в Главное меню
        private void ButtonBackToMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageMainMenu());
        }

        private void PopulateDataGrid()
        {
            // Создаем колонки
            DataGridTextColumn column1 = new DataGridTextColumn();
            column1.Header = "ИНН";
            column1.Binding = new Binding("Column1");

            DataGridTextColumn column2 = new DataGridTextColumn();
            column2.Header = "Наименование";
            column2.Binding = new Binding("Column2");

            DataGridTextColumn column3 = new DataGridTextColumn();
            column3.Header = "Город";
            column3.Binding = new Binding("Column3");

            DataGridTextColumn column4 = new DataGridTextColumn();
            column4.Header = "Область";
            column4.Binding = new Binding("Column4");

            // Добавляем колонки и устанавливаем источник данных для DataGrid
            DataGridMainMenu.Columns.Add(column1);
            DataGridMainMenu.Columns.Add(column2);
            DataGridMainMenu.Columns.Add(column3);
            DataGridMainMenu.Columns.Add(column4);

            // Доступ к БД
            CompanyDataAccess CDA = new CompanyDataAccess();

            // Хранит список строк с данными
            List<DataItem> dataItemList = new List<DataItem>();
            
            // Какие компании загрузить
            if (_showOpenedCompanies)
            {
                dataItemList = Convert_ListOfListsStr2ListDataItem(CDA.GetOpenCompanies());
            }
            else
            {
                dataItemList = Convert_ListOfListsStr2ListDataItem(CDA.GetClosedCompanies());
            }

            // Установить список данных как источник для Datagrid
            DataGridMainMenu.ItemsSource = dataItemList;
        }

        // Преобразовать List<List<string>> в List<DataItem>
        private List<DataItem> Convert_ListOfListsStr2ListDataItem(List<List<string>> LL)
        {
            List<DataItem> dataItemList = new List<DataItem>();
            foreach (List<string> L in LL)
            {
                DataItem di = new DataItem { Column1 = L[0], Column2 = L[1], Column3 = L[2], Column4 = L[3] };
                dataItemList.Add(di);
            }
            return dataItemList;
        }
    }
}
