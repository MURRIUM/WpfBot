using System;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfBot.Bot;

namespace WpfBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SoundPlayer sp;
        BotMain botAI;
        bool studyingMod = false, isHuman = true;
        string userSays;

        public MainWindow()
        {
            InitializeComponent();
            botAI = new BotMain();
            sp = new SoundPlayer(@"Nani.wav");
            using (StreamWriter log = File.CreateText("log.txt"))
            {
                log.WriteLine("Последняя сессия:");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            
            //Отправка сообщения
            if (textBox1.Text != "")
            {
                if(botAI.Censor(textBox1.Text) == "Ты написал мне плохое сообщение!")
                {
                    sp.Play();
                    createMessage(botAI.Censor(textBox1.Text), false);
                }
                else if(studyingMod && textBox1.Text == "studyingMod.deactivate")
                {//Выключение режима обучения
                    studyingMod = false;
                    createMessage("Режим обучения выключен", false);
                }
                else if (studyingMod && isHuman && botAI.Answer(textBox1.Text) == "Извините, я вас не понимаю")
                {//Обучение сообщению пользователя
                    createMessage(textBox1.Text, true);
                    userSays = textBox1.Text;
                    createMessage("Введите текст, которым я должен ответить:", false);
                    isHuman = false;
                }
                else if (studyingMod && !isHuman)
                {//Обучение ответу
                    createMessage(textBox1.Text, false);
                    botAI.studyingBot(userSays, textBox1.Text);
                    createMessage("Введите текст, на который я должен дать ответ:" , false);
                    userSays = "";
                }
                else if (textBox1.Text == "studyingMod.activate")
                {//Включение режима обучения
                    studyingMod = true;
                    createMessage("Приветствуем в программе обучения!", false);
                    createMessage("Введите текст, на который я должен дать ответ:", false);
                }
                else if (textBox1.Text != "")
                {//Механизм общения с ботом
                    createMessage(textBox1.Text, true);
                    createMessage(botAI.Answer(textBox1.Text), false);
                }
                textBox1.Text = "";
            }
        }

        private void textBox1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click(sender, e);
            }
        }

        private void createMessage(string str, bool isHuman)
        {
            //логгирование текущей сессии
            using (StreamWriter log = File.AppendText("log.txt"))
            {
                log.WriteLine(str);
            }
            StackPanel all = new StackPanel();

            //Создание оформления текста
            Border Mess = new Border();
            Mess.BorderBrush = Brushes.DarkCyan;
            Mess.BorderThickness = new Thickness(1);
            Mess.Background = Brushes.DarkCyan;
            Mess.CornerRadius = new CornerRadius(5);
            Mess.Margin = new Thickness(0, 0, 0, 5);

            TextBox txtToAdd = new TextBox();
            txtToAdd.Text = str;
            txtToAdd.IsEnabled = false;
            txtToAdd.FontSize = 15;
            txtToAdd.FontWeight = FontWeights.Black;
            txtToAdd.FontWeight = FontWeights.Bold;
            txtToAdd.TextWrapping = TextWrapping.Wrap;
            Mess.Child = txtToAdd;

            //определение стороны вывода
            if(isHuman)
            {
                Mess.HorizontalAlignment = HorizontalAlignment.Right;
                Mess.Margin = new Thickness(20, 0, 7, 15);
            }
            else
            {
                Mess.HorizontalAlignment = HorizontalAlignment.Left;
                Mess.Margin = new Thickness(1, 0, 2, 15);
            }
            //добавление элемента
            myStackPanel.Children.Add(Mess);
        }
    }
}
