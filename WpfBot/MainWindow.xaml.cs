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

                Border circleImg = new Border();
                circleImg.CornerRadius = new CornerRadius(25);
                circleImg.Width = 50;
                circleImg.Height = 50;

                BitmapImage img = new BitmapImage(new Uri(@"pack://application:,,,/WpfBot;component/Images/cat1.jpg"));
                if (isHuman)
                    img = new BitmapImage(new Uri(@"pack://application:,,,/WpfBot;component/Images/cat1.jpg"));
                else
                    img = new BitmapImage(new Uri(@"pack://application:,,,/WpfBot;component/Images/cat2.jpg"));

                ImageBrush image = new ImageBrush();
                image.ImageSource = img;
                circleImg.Background = image;
                myStackPanel.Children.Add(circleImg);

                if (isHuman)
                    circleImg.HorizontalAlignment = HorizontalAlignment.Left;
                else
                    circleImg.HorizontalAlignment = HorizontalAlignment.Right;

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
                Mess.Child = txtToAdd;
                //Велосипед отоброжения типа бот
                if (isHuman)
                {
                    Mess.HorizontalAlignment = HorizontalAlignment.Left;
                    Mess.Margin = new Thickness(51, 0, 0, 5);
                }
                else
                {
                    Mess.HorizontalAlignment = HorizontalAlignment.Right;
                    Mess.Margin = new Thickness(0, 0, 51, 5);
                }

                myStackPanel.Children.Add(Mess);
                textBox1.Text = "";
                isHuman = !isHuman;
            }
        }
    }
}
