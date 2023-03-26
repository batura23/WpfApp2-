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
using WpfApp2ПРАВИЛЬНЫЙ.Classes;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp2ПРАВИЛЬНЫЙ.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Classes.Sale ras;



        public MainWindow()
        {
            InitializeComponent();

            ras = new Sale(Convert.ToDecimal(46.90), Convert.ToDecimal(12.90), Convert.ToDecimal(53.5));
            Карандаш.IsChecked = false;
            Тетрадь.IsChecked = false;
            Альбом.IsChecked = false;
            Set(false);
        }

        //private decimal Карандаш = 46.9;



        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (КарандашРасчет.Visibility == Visibility.Visible)
            {
                Set(false);
            }
            if (Деньги.Text == "")
            {
                MessageBox.Show("Внесите сумму");
                return;
            }
            if (Карандаш.IsChecked == false && Тетрадь.IsChecked == false && Альбом.IsChecked == false)
            {
                MessageBox.Show("Выберите товар");
                return;
            }
            decimal price = ras.Расчет(Convert.ToBoolean(Карандаш.IsChecked), Convert.ToBoolean(Тетрадь.IsChecked), Convert.ToBoolean(Альбом.IsChecked));


            decimal money;

            try
            {
                money = Convert.ToDecimal(Деньги.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Введите правмльно сумму");
                return;
            }

            if (money < price)
            {
                MessageBox.Show("Денег не хватает");
                return;
            }
            СтоимостьРасчет.Content = $"Стоимость заказа {price} рублей";
            СдачаРасчет.Content = $"Ваша сдача {money - price} рублей";
            Set(true);

            var helper = new WordHelper("Печать.docx");

            var items = new Dictionary<string, string>
            {
                {"<Внесенное>",Деньги.Text},
                {"<Стоимость>",Convert.ToString(price)},
                {"<Сдача>", Convert.ToString(money-price)},
                {"<Товар>", Convert.ToString(Карандаш)}

            };
            helper.Process(items);
        }
        private void Set(bool карандаш)
        {
            if (карандаш)
            {
                if (Карандаш.IsChecked == true)
                {
                    КарандашРасчет.Visibility = Visibility.Visible;
                }
                if (Тетрадь.IsChecked == true)
                {
                    ТетрадьРасчет.Visibility = Visibility.Visible;
                }
                if (Альбом.IsChecked == true)
                {
                    АльбомРасчет.Visibility = Visibility.Visible;
                }
                СтоимостьРасчет.Visibility = Visibility.Visible;
                СдачаРасчет.Visibility = Visibility.Visible;
            }
            else
            {
                КарандашРасчет.Visibility = Visibility.Hidden;
                ТетрадьРасчет.Visibility = Visibility.Hidden;
                АльбомРасчет.Visibility = Visibility.Hidden;
                СтоимостьРасчет.Visibility = Visibility.Hidden;
                СдачаРасчет.Visibility = Visibility.Hidden;
            }
        }
        private void FindAndReplace(Word.Application wordApp, object findText, object replaceText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref findText, ref matchCase,
                ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format,
                ref replaceText, ref replace, ref matchKashida,
                ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}
