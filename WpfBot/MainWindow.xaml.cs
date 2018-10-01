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

namespace WpfBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isHuman = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Отправка сообщения
            if (textBox1.Text != "")
            {
                StackPanel all = new StackPanel();

                Border Mess = new Border();
                Mess.BorderBrush = Brushes.DarkCyan;
                Mess.BorderThickness = new Thickness(1);
                Mess.Background = Brushes.DarkCyan;
                Mess.CornerRadius = new CornerRadius(5);
                Mess.Margin = new Thickness(0, 0, 0, 5);
                

                TextBox txtToAdd = new TextBox();
                txtToAdd.Text = textBox1.Text;
                txtToAdd.IsEnabled = false;
                txtToAdd.FontSize = 15;
                txtToAdd.FontWeight = FontWeights.Black;
                txtToAdd.FontWeight = FontWeights.Bold;
                txtToAdd.TextWrapping = TextWrapping.Wrap;
                Mess.Child = txtToAdd;
                //Велосипед отоброжения типа бот
                if (isHuman)
                {
                    Mess.HorizontalAlignment = HorizontalAlignment.Left;
                    Mess.Margin = new Thickness(1, 0, 25, 15);
                }
                else
                {
                    Mess.HorizontalAlignment = HorizontalAlignment.Right;
                    Mess.Margin = new Thickness(20, 0, 7, 15);
                }

                myStackPanel.Children.Add(Mess);
                textBox1.Text = "";
                isHuman = !isHuman;
            }
        }
    }
}
