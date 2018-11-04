using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfBot.Bot;
using static WpfBot.materials.FlashWindowHelper;

namespace WpfBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SoundPlayer matSound, knockSound, missingUser, incorrectSound;
        BotMain botAI;
        bool studyingMod = false, isHuman = true;
        string userSays;
        private DispatcherTimer timer1 = null;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
            botAI = new BotMain();
            matSound = new SoundPlayer(@"oiio.wav");
            knockSound = new SoundPlayer(@"vknock.wav");
            missingUser = new SoundPlayer(@"qq.wav");
            incorrectSound = new SoundPlayer(@"IncorrectEnter.wav");
            timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timerTick);
            timer1.Interval = new TimeSpan(0, 0, 0, 120, 0);
            timer1.Start();
            
            using (StreamWriter log = File.CreateText("log.txt"))
            {
                log.WriteLine("Последняя сессия:");
            }
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Ты хочешь уйти?", "Закрытие", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                e.Cancel = true;
            }
            else
            {
                //do yes stuff
                MessageBox.Show("Пока");
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            this.FlashWindow();
            missingUser.Play();
            createMessage("Тебя давно не было?", false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Отправка сообщения
            if (textBox1.Text != "")
            {
                if(botAI.Censor(textBox1.Text) == "Ты написал мне плохое сообщение!")
                {
                    matSound.Play();
                    createMessage(botAI.Censor(textBox1.Text), false);
                }
                else if(studyingMod && textBox1.Text == "studyingMod.deactivate")
                {//Выключение режима обучения
                    studyingMod = false;
                    createMessage("Режим обучения выключен", false);
                    knockSound.Play();
                }
                else if (studyingMod && isHuman && botAI.Answer(textBox1.Text) == "Я не понимаю, о чем ты говоришь")
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
                    if (botAI.Answer(textBox1.Text) == "Я не понимаю, о чем ты говоришь") 
                        incorrectSound.Play();
                    else
                        knockSound.Play();
                    this.FlashWindow();
                }
                textBox1.Text = "";
            }

            timer1.Stop();
            timer1.Interval = new TimeSpan(0, 0, 0, 120, 0);
            timer1.Start();
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
            txtToAdd.FontSize = 16;
            txtToAdd.FontFamily = new FontFamily("Times New Roman");
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
