using System;
using Discord;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using WolframAlphaNET.Objects;

namespace Accord
{
    static class Commands
    {
        public static bool isCleverbotToggled = true;
        public static bool isWolframToggled = true;

        static Random rnd = new Random();
        public static string output = "";
        public static Int64 rrerrCount = 0;
        public static string DoCah(int type)
        {
            string whitefile = "";
            string blackfile = "";
            switch (type)
            {
                case 1:
                    whitefile = "whitecards";
                    blackfile = "blackcards";
                    break;
                case 2:
                    whitefile = "whitecards_dh";
                    blackfile = "blackcards_dh";
                    break;
                case 3:
                    string[] dh = "_dh|".Split('|');
                    whitefile = "whitecards" + dh[rnd.Next(dh.Length)];
                    blackfile = "blackcards";
                    if (whitefile != "whitecards_dh")
                    {
                        blackfile = "blackcards_dh";
                    }
                    break;

            }
            string[] readWhite = File.ReadAllLines(@"commands/" + whitefile + ".txt");
            string[] readBlack = File.ReadAllLines(@"commands/" + blackfile + ".txt");
            string blackCard = readBlack[rnd.Next(readBlack.Length)];
            if (blackCard.Contains("_"))
            {
                blackCard = Regex.Replace(blackCard, "_", m => readWhite[rnd.Next(readWhite.Length)]);
            }
            else
            {
                blackCard = blackCard + " " + readWhite[rnd.Next(readWhite.Length)];
            }
            return blackCard;
        }
        public static string Cleverbot(string message)
        {
            
                try
                {
                    output = Program._cleverbot.Think(message);
                }
                catch (Exception)
                {
                    string[] res = File.ReadAllLines(@"commands/Cleverbot404.txt");
                    output = res[rnd.Next(res.Length)];
                }
            return output;
        }
        public static string Wolfram(string input)
        {
            output = "Sorry, your search returned no results.";
            QueryResult results = Program._wolfram.Query(input.Replace("!wolfram ", string.Empty));
            try
            {
                if (results != null)
                {
                    foreach (Pod pod in results.Pods)
                    {
                        try
                        {
                            if (pod.Position == 200)
                            {
                                if (pod.SubPods != null)
                                {
                                    string s = pod.SubPods[0].Plaintext;

                                    if (!string.IsNullOrWhiteSpace(s) | !string.IsNullOrEmpty(s))
                                    {
                                        if (s.Contains("\n"))
                                        {
                                            string[] asd = s.Split('\n');
                                            s = string.Join("", asd);
                                            Regex.Replace(s, "[ ]{2,}", "");
                                        }


                                    }
                                    else
                                    {
                                        while (string.IsNullOrWhiteSpace(s))
                                        {
                                            int i = 0;
                                            s = pod.SubPods[i].Plaintext;
                                            i++;
                                        }
                                    }
                                    output = s;

                                }
                            }
                        }
                        catch (Exception)
                        {
                            output = "you broke wolfram good job";
                        }

                    }
                }
            }
            catch (Exception)
            {
                output = "you broke wolfram good job";
            }
            return output;
        }
        public static string EightBall()
        {
            string[] answer = File.ReadAllLines(@"commands/eightball.txt");

            return answer[rnd.Next(answer.Length)];
        }
        public static void Music(string message)
        {
            string url = message.Replace("!music ", string.Empty);
            if (url.Contains("soundcloud.com") | url.Contains("youtube.com/watch") | url.Contains("youtu.be/"))
            {
                string img = url;
                File.AppendAllText(@"commands/music.txt", Environment.NewLine + img);
            }
            else
            {
                LinkMusic();
            }
        }
        public static string LinkMusic()
        {
            string[] urls = File.ReadAllLines(@"commands/music.txt");
            output = urls[rnd.Next(urls.Length)];
            return output;
        }
    }
}
