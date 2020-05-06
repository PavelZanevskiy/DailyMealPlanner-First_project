using Business_Layer;
using Data_Objects;
using Services;
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

namespace DailyMealProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Service service = new Service();
        public MainWindow()
        {
            InitializeComponent();
            cmbActivity.ItemsSource = service.GetActivity();
            cmbActivity.SelectedValue = service.DefaultActivity();
            prodLV.ItemsSource = service.GetProductsToList();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(prodLV.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            view.GroupDescriptions.Add(groupDescription);
            view.Filter = UserFilter;
            tvRation.ItemsSource = cmbMeals.ItemsSource = service.GetRation();
            cmbMeals.SelectedValue = service.GetDefaultMeal();
           
        }


        private bool UserFilter(object item)
        {
            
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Product).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            CollectionViewSource.GetDefaultView(prodLV.ItemsSource).Refresh();
        }

        private void prodLV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            double temp;
            if (cmbMeals.SelectedItem != null && prodLV.SelectedIndex!=-1 && calInfo.Text!="")
            {
                infoPanel.DataContext = null;
                Product product = service.GetProduct(prodLV.SelectedItem as Product);
                prodLV.SelectedValue = null;
                MealTime meal = (cmbMeals.SelectedItem as MealTime);
               
                temp = service.CheckCaloriesSum(product);
                if (temp > pbCalories.Maximum)
                {
                    MessageBox.Show("Переполнение рациона", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    tvRation.ItemsSource = service.AddProduct(product, meal.Name);
                   // tvRation.Items.Refresh();
                    pbCalories.Value = service.GetCaloriesSum();
                    check1.Text = service.GetCaloriesSum().ToString();
                }                
            }
        }

        private void prodLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = service.GetProduct(prodLV.SelectedItem as Product);
            infoPanel.DataContext = product;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (addMeal.Text != "")
            {
                tvRation.ItemsSource = service.AddMealTime(addMeal.Text);
                tvRation.Items.Refresh();                
                cmbMeals.SelectedValue = service.GetDefaultMeal();                
                cmbMeals.Items.Refresh();
            }
        }

        private void tvRation_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {            
            if (tvRation.SelectedItem != null)
            {
                if ((tvRation.SelectedItem is MealProduct))
                {
                    MealProduct product = (tvRation.SelectedItem as MealProduct);
                    tvRation.ItemsSource = service.RemoveProduct(product.ID);
                    pbCalories.Value = service.GetCaloriesSum();
                    check1.Text = service.GetCaloriesSum().ToString();
                    infoPanel.DataContext = null;
                    cmbMeals.Items.Refresh();
                }
                else
                {
                    MealTime meal = (tvRation.SelectedItem as MealTime);
                    tvRation.ItemsSource = service.RemoveMealTime(meal.Name);
                    cmbMeals.SelectedValue = service.GetDefaultMeal();
                    pbCalories.Value = service.GetCaloriesSum();
                    check1.Text = service.GetCaloriesSum().ToString();
                    infoPanel.DataContext = null;
                    tvRation.Items.Refresh();
                    cmbMeals.Items.Refresh();
                }
            }
        }

        private void tvRation_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {            
            if (tvRation.SelectedItem is MealProduct)
            {
                MealProduct product = service.GetMealProduct((MealProduct)tvRation.SelectedItem);
                infoPanel.DataContext = product;
                sl.Value = product.Grams;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool cond1=true;
            bool cond2=true;
            bool cond3=true;
            if (weightInfo.Text != "" && heightInfo.Text != "" && ageInfo.Text != "")
            {
                double num;
                cond1 = double.TryParse(weightInfo.Text, out num);
                cond2 = double.TryParse(heightInfo.Text, out num);
                cond3 = double.TryParse(ageInfo.Text, out num);
            }
            if (cond1 && cond2 && cond3)
            {
                if (weightInfo.Text != "" && heightInfo.Text != "" && ageInfo.Text != "")
                {
                    bmrInfo.Text = service.GetBMR(service.SetUserInfo(Convert.ToDouble(weightInfo.Text), Convert.ToDouble(heightInfo.Text), Convert.ToInt32(ageInfo.Text)));
                    calInfo.Text = service.GetCalories(service.GetUser());
                    pbCalories.Maximum = Convert.ToDouble(service.GetCalories(service.GetUser()));
                    check.Text = pbCalories.Maximum.ToString();

                }
                else
                {
                    bmrInfo.Clear();
                }
            }
            else
            {
                MessageBox.Show("Введён текст!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbActivity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            armInfo.Text = service.GetARM(service.SetActivity((User.Activity)cmbActivity.SelectedItem));
            if (weightInfo.Text != "" && heightInfo.Text != "" && ageInfo.Text != "")
            {
                calInfo.Text = service.GetCalories(service.GetUser());
                pbCalories.Maximum = Convert.ToDouble(service.GetCalories(service.GetUser()));
                check.Text = pbCalories.Maximum.ToString();
            }

        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            service.ClearRation();
            tvRation.Items.Refresh();
            cmbMeals.Items.Refresh();
            pbCalories.Value = service.GetCaloriesSum();
        }

        
        private void sl_MouseLeave(object sender, MouseEventArgs e)
        {
            if(tvRation.SelectedItem is MealProduct && tvRation.SelectedItem != null)
            {
                MealProduct product = service.GetMealProduct((MealProduct)tvRation.SelectedItem);
                double temp = service.TestCalories(product.ID, sl.Value, product.Name);
                if (temp > pbCalories.Maximum)
                {
                    MessageBox.Show("Переполнение рациона");
                }
                else
                {
                    service.ChangeWeight(product.ID, sl.Value, product.Name);
                    pbCalories.Value = service.GetCaloriesSum();
                    check1.Text = service.GetCaloriesSum().ToString();
                    infoPanel.DataContext = product;
                    
                    //tvRation.Items.Refresh();
                }


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            service.SerializeRation();
            MessageBox.Show("Ваш рацион сохранён", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }       
       
    }
}
