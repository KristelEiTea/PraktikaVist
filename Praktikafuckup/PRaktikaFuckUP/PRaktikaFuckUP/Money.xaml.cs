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
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;

namespace PRaktikaFuckUP
{
    /// <summary>
    /// Interaction logic for Money.xaml
    /// </summary>
    public partial class Money : Window
    {
        private string MonthMoneyEX = "ex. 500";
        private string BigName = "ex. Rent";
        private string BigCost = "ex. 350";
        public double MonthlyMoney;
        public List<Planner> items = new List<Planner>();
        public Money()
        {
            int dateall = 0;
            InitializeComponent();
            for (int i = 0; i < 31; i++)
            {
                dateall += 1;
                items.Add(new Planner() { Date = dateall });
            }
            var tmpitems = ReadDataFromFile();
            items = tmpitems.ToList();
            PlannerListBox.ItemsSource = items;
        }

        private void OpenList_Click(object sender, RoutedEventArgs e)
        {
            ShopList shoplist = new ShopList();
            shoplist.Show();
            shoplist.ShopListDay.Text = 0.ToString();
        }

        private void Ref_Click(object sender, RoutedEventArgs e)
        {
            PlannerListBox.Items.Refresh();
        }

        public class Planner
        {
            public int Date { get; set; }
            public double Limit { get; set; }
            public double Used { get; set; }
            public double Leftover { get; set; }
        }

        private void MoneyUsedOK_Click(object sender, RoutedEventArgs e)
        {

            
                double MoneyUsedThatDay = double.Parse(MoneyUsedThisDay.Text);
                PlannerListBox.Items.Refresh();
                (PlannerListBox.SelectedItem as Planner).Used = MoneyUsedThatDay;
                double Remaining = double.Parse(OneMonthMoney.Text) - double.Parse(BigOutGoesCost.Text);
                double OneMonth = Remaining / 31;
                (PlannerListBox.SelectedItem as Planner).Leftover = OneMonth - MoneyUsedThatDay;
            
        }

        private void BIgOutGoesOK_Click(object sender, RoutedEventArgs e)
        {
            double Remaining = double.Parse(OneMonthMoney.Text) - double.Parse(BigOutGoesCost.Text);
            textBox.Text = Remaining.ToString();
            double OneMonth = Remaining / 31;
            int numbbs = 0;
            for (int i = 0; i < 31; i++)
            {
                items[numbbs].Limit = OneMonth;
                numbbs += 1;
            }
            PlannerListBox.Items.Refresh();
        }

        private void OneMonthMoney_GotFocus(object sender, RoutedEventArgs e)
        {
            if (OneMonthMoney.Text == MonthMoneyEX)
                OneMonthMoney.Text = "";
        }

        private void OneMonthMoney_LostFocus(object sender, RoutedEventArgs e)
        {
            if (OneMonthMoney.Text == "")
                OneMonthMoney.Text = MonthMoneyEX;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void save_Click_1(object sender, RoutedEventArgs e)
        {
            SaveDataToFile();
            Environment.Exit(1);
        }

        private void SaveDataToFile()
        {
            var json = JsonConvert.SerializeObject(PlannerListBox.Items);
            File.WriteAllText("PlannerListBoxData.txt", json);
        }

        private IEnumerable<Planner> ReadDataFromFile()
        {
            var json = File.ReadAllText("PlannerListBoxData.txt");
            var data = JsonConvert.DeserializeObject<IEnumerable<Planner>>(json);
            return data;
        }

        private void BigOutGoesName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BigOutGoesName.Text == BigName)
                BigOutGoesName.Text = "";
        }

        private void BigOutGoesName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BigOutGoesName.Text == "")
                BigOutGoesName.Text = BigName;
        }

        private void BigOutGoesCost_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BigOutGoesName.Text == BigCost)
                BigOutGoesName.Text = "";
        }

        private void BigOutGoesCost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (BigOutGoesName.Text == "")
                BigOutGoesName.Text = BigCost;
        }
    }
}
