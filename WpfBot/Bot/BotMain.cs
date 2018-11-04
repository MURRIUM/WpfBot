using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfBot.Bot
{
    class BotMain
    {

        List<string> baze = new List<string>();
        static Regex ansMatch;
        string toTrim = "(){}:!?&@#$%^&*`~'\"\\/,.<>-_=+*";

        public BotMain()
        {
            baze.AddRange(File.ReadAllLines("baza.txt"));
        }


        public string Answer(string str)
        {
            str = str.ToLower();
            str = str.Trim(toTrim.ToCharArray());
            if(Censor(str) == "Ты написал мне плохое сообщение!")
            {
                return str;
            }
            for(int i = 0; i < baze.ToArray().Length; i += 2)
            {
                ansMatch = new Regex(baze[i].ToLower());
                if(ansMatch.IsMatch(str))
                {
                    return baze[i + 1];
                }
            }

            str = "Я не понимаю, о чем ты говоришь";
            return str;
        }

        public void studyingBot(string userStr, string botStr)
        {
            
            baze.Add(userStr.Trim(toTrim.ToCharArray()));
            baze.Add(botStr);
            File.WriteAllLines("baza.txt", baze.ToArray());
            
        }

        public string Censor(string mes)
        {
            Regex mat = new Regex(@"бля|блядь|ебал|ебать|xyй|ахуел|ахуеть|бляди|блядина|въеб|въебался|выблядок|выебать|выебон|выебываться|гавно|гавнюк|гандон|гнида|долбоеб|долбоёб|ебало|ебанный|еблан|заебись|лох|лошара|мразь|мудак|мудила|не хер|не хуй|наебать|наебет|напиздел|насрать|нахер|нахрен|нахуй|не ебет|нехрен|нехуй|нихуя|отъебись|охуевать|охуел|охуенно|охуеть|охуительно|падла|подонки|подонок|паскуда|пидар|пидарaс|пидарасы|пиздеть|пиздец|пиздит|пиздить|похуй|похую|похер|похрен|похрену|похуй|приебаться|сука|суки|сучка|съебаться|ублюдок|уебать|хер|херня|херовато|хуево|хуевый|хуёвый|хуесос|хуета|хуею|хуи|хуй|чмо|чмошник|шлюха", RegexOptions.IgnoreCase);
            if ((mat.IsMatch(mes)) == true)
                return "Ты написал мне плохое сообщение!";
            else return mes;
        }
    }
}
