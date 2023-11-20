using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls.Primitives;

namespace PrimKyrs1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //конструктор
        MyCount2Context db = new MyCount2Context();        
        DataSet set = null;        
        public MainWindow()
        {
            InitializeComponent();
            GetAllCountries();
            Loaded += MainWindow_Loaded;
            
            //заполнения данными ListBox
            List<string> data = new List<string>
            {
                "Название столиц начинается с буквы А",
                "Название всех европейских стран",
                "Название стран с площадью большей конкретного числа",
                "Название стран, у которых количество жителей больше указанного числа",
                "Страны, у которых название начинается с буквы А",
                "Название стран, у которых площадь находится в указанном диапазоне",
                "Название стран, у которых количество жителей находится в указанном диапазоне",
                "Страна с самой большой площадью",
                "Стран с самым большим количеством жителей"

            };
            myListBox.ItemsSource = data;
        }
        //Выгружаются данные в локальный контекст
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            db.Countries.Load();
            DataContext = db.Countries.Local.ToObservableCollection();
        }
        /// <summary>
        /// Заполнение строки в DataGrid  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAllCountries()
        {
            using (MyCount2Context db = new MyCount2Context())
            {
                var countriesList = db.Countries.ToList();
                countriesDataGrid.ItemsSource = countriesList;
            }
        }
        /// <summary>
        //Обработка кнопки добавить
        /// <summary>
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Count newCount = new Count()
            {
                Country = countryTextBox.Text,
                Capital = capitalTextBox.Text,
                Continent = continentTextBox.Text,
                Population = Convert.ToInt32(populationTextBox.Text),
                Area = Convert.ToInt32(areaTextBox.Text),
            };
            db.Countries.Add(newCount);
            db.SaveChanges();
            countriesList.Items.Refresh();
            GetAllCountries();
        }
        /// <summary>
        //Обработка кнопки удалить
        /// <summary>
        private void delBtn_Click(object sender, RoutedEventArgs e)
        {

            Count? count = countriesList.SelectedItem as Count;
            if (count is null)
                return;
            db.Countries.Remove(count);
            db.SaveChanges();
            countriesList.Items.Refresh();
            GetAllCountries();
        }
        /// <summary>
        //Обработка кнопки изменить
        /// <summary>
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            Count? count = countriesList.SelectedItem as Count;
            if (count is null) return;
            count.Country = countryTextBox.Text;
            count.Capital = capitalTextBox.Text;
            count.Continent = continentTextBox.Text;
            count.Population = Convert.ToInt32(populationTextBox.Text);
            count.Area = Convert.ToInt32(areaTextBox.Text);

            count = db.Countries.Find(count.Id);
            db.SaveChanges();
            countriesList.Items.Refresh();
            GetAllCountries();

        }
        /// <summary>
        /// Обрабока события на выбор строки в countriesList  
        /// </summary>
        private void countriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Count? count = countriesList.SelectedItem as Count;
            if (count is null) return;
            countryTextBox.Text = count.Country;
            capitalTextBox.Text = count.Capital;
            continentTextBox.Text = count.Continent;
            populationTextBox.Text = count.Population.ToString();
            areaTextBox.Text = count.Area.ToString();
        }
        /// <summary>
        //Обработка запросов в myListBox
        /// <summary>
        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = myListBox.SelectedIndex;
            
                // MessageBox.Show(selectedIndex.ToString());
                if (selectedIndex != -1)
                {// проверка индекса выбранного элемента
                    switch (selectedIndex)
                    {
                        case 0:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Capital.StartsWith("А")).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 1:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Continent.StartsWith("Е")).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 2:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Area > 100000).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 3:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Population > 100000).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 4:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Country.StartsWith("А")).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 5:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Area > 100000 && e.Area < 1000000).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 6:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            var countries = db.Countries.Where(e => e.Population > 1000 && e.Population < 100000).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 7:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            int MaxArea = (int)db.Countries.Max(e => e.Area);
                                var countries = db.Countries.Where(e => e.Area == MaxArea).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        case 8:
                        dataGrid.ItemsSource = null;
                        using (MyCount2Context db = new MyCount2Context())
                        {
                            int MaxPopilation = (int)db.Countries.Max(p => p.Population);
                                var countries = db.Countries.Where(p => p.Population == MaxPopilation).ToList();
                            dataGrid.ItemsSource = countries;
                        }
                        break;
                        default:
                            // Выполнить действия, если ??
                        break;                       
                    }
                }            
        }
        private void ToList()
        {
            throw new NotImplementedException();
        }
    }
}
