using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfBot.Bot;
using System.Text.RegularExpressions;

namespace Tests
{
    
    [TestClass]
    public class Test_Censor      // FR-SWR-1-8-3 - проверка цензуры
    {
        [TestMethod]
        public void Censor_blya_error()  
        {
            // исходные данные
            string mes = "Блядина, уйди!";
            string expected = "Ты написал мне плохое сообщение!";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Censor(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Censor_hyi_error()
        {
            // исходные данные
            string mes = "Вот это ХУЙ!";
            string expected = "Ты написал мне плохое сообщение!";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Censor(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Censor_mes1_pass()
        {
            // исходные данные
            string mes = "Хорошо, когда есть кот.";
            string expected = mes;

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Censor(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }

    }

    [TestClass]
    public class Test_Answer_Symb      // FR-SWR-1-7-1 - проверка на ввод случайного набора символов
    {
        [TestMethod]
        public void Answer_symb1_error()
        {
            // исходные данные
            string mes = "аааааншолрдшрдш";
            string expected = "Я не понимаю, о чем ты говоришь";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Answer(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Answer_symb2_error()
        {
            // исходные данные
            string mes = "..................";
            string expected = "Я не понимаю, о чем ты говоришь";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Answer(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Answer_symb3_error()
        {
            // исходные данные
            string mes = "аырпдпыр!)";
            string expected = "Я не понимаю, о чем ты говоришь";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Answer(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }

    }

    [TestClass]
    public class Test_Answer_Rob  // FR-SWR-1-17-2 - проверка ответа на впорос "Ты кто?"
    {
        [TestMethod]
        public void Answer_q1_pass()
        {
            // исходные данные
            string mes = "кто ты";
            string expected = "Я программа, написанная человеком, не имеющая расы или гендерной принадлежности";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Answer(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Answer_q2_pass()
        {
            // исходные данные
            string mes = "Ты кто?";
            string expected = "Я программа, написанная человеком, не имеющая расы или гендерной принадлежности";

            // получение значения с помощью тестируемого метода
            BotMain bm = new BotMain();
            string actual = bm.Answer(mes);

            // сравнение ожидаемого результата с полученным
            Assert.AreEqual(expected, actual);
        }

    }

    [TestClass]
    public class Test_Censor_Word  // NFR-SWR-4-4-1 - проверка состава словаря цензуры
    {

        [TestMethod]
        public void Censor_words()
        {
            string[] test = {
            "бля", "блядь", "ебал", "ебать", "xyй", "ахуел", "ахуеть", "бляди", "блядина", "въеб", "въебался", "выблядок", "выебать", "выебон", "выебываться", "гавно", "гавнюк", "гандон", "гнида", "долбоеб", "долбоёб", "ебало", "ебанный", "еблан", "заебись", "лох", "лошара", "мразь", "мудак", "мудила", "на хер", "на хуй", "наебать", "наебет", "напиздел", "насрать", "нахер", "нахрен", "нахуй", "не ебет", "нехрен", "нехуй", "нихуя", "отъебись", "охуевать", "охуел", "охуенно", "охуеть", "охуительно", "падла", "подонки", "подонок", "паскуда", "пидар", "пидарaс", "пидарасы", "пиздеть", "пиздец", "пиздит", "пиздить", "похуй", "похую", "похер", "похрен", "похрену", "похуй", "приебаться", "сука", "суки", "сучка", "съебаться", "ублюдок", "уебать", "хер", "херня", "херовато", "хуево", "хуевый", "хуёвый", "хуесос", "хуета", "хуею", "хуи", "хуй", "чмо", "чмошник", "шлюха"
            };

            int expected = 0;
            int actual = 0;

            for (int i = 0; i < test.Length; i++)
                expected++;

            MatchCollection matches;

            Regex mat = new Regex(@"бля|блядь|ебал|ебать|xyй|ахуел|ахуеть|бляди|блядина|въеб|въебался|выблядок|выебать|выебон|выебываться|гавно|гавнюк|гандон|гнида|долбоеб|долбоёб|ебало|ебанный|еблан|заебись|лох|лошара|мразь|мудак|мудила|не хер|не хуй|наебать|наебет|напиздел|насрать|нахер|нахрен|нахуй|не ебет|нехрен|нехуй|нихуя|отъебись|охуевать|охуел|охуенно|охуеть|охуительно|падла|подонки|подонок|паскуда|пидар|пидарaс|пидарасы|пиздеть|пиздец|пиздит|пиздить|похуй|похую|похер|похрен|похрену|похуй|приебаться|сука|суки|сучка|съебаться|ублюдок|уебать|хер|херня|херовато|хуево|хуевый|хуёвый|хуесос|хуета|хуею|хуи|хуй|чмо|чмошник|шлюха", RegexOptions.IgnoreCase);
           
            for (int i = 0; i < test.Length; i++)
            {
                matches = mat.Matches(test[i]);
                if (matches.Count > 0)
                    actual++;
            }

            Assert.AreEqual(expected, actual);


        }
    }


}
