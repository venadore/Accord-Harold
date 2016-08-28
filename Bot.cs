using Discord;
using Accord;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;

namespace Accord
{
    //This is where all the magic happens.

    public static class Bot
    {
        //Run the bot
        public static DiscordClient bot;
        public static void RunBot()
        {
            //First create a new client
            bot = new DiscordClient();
            //Hook MessageReceived to onMessage
            bot.MessageReceived += onMessage;
            //And connect      
            bot.ExecuteAndWait(async () =>
            {
                while (true)
                {
                    try
                    {
                        Commands.rrerrCount = Convert.ToInt64(File.ReadAllText(@"Commands/rrerr.txt"));
                        await bot.Connect(Json.token);
                        break;
                    }
                    catch
                    {
                        await Task.Delay(3000);
                    }
                }
            });
        }

        //Detect messages
        public static void onMessage(object sender, MessageEventArgs e)
        {
            #region rrerr
            if (Utils.Contains(e.Message.Text, "rrerr", StringComparison.OrdinalIgnoreCase) && !e.Message.Text.StartsWith("!rrerr") && !e.Message.User.IsBot)
            {
                bool FiftyLeft = false;
                string total = Convert.ToString(Commands.rrerrCount);
                long lastdigits = Commands.rrerrCount % 100;
                if (lastdigits == Utils.Clamp(lastdigits, 50, 99))
                {
                    FiftyLeft = true;
                }
                if (total.EndsWith("000"))
                {
                    e.Channel.SendMessage(e.Message.User.Mention + " just hit the " + Commands.rrerrCount + "th rrerr! Congratulations!");
                }

                if (Utils.Contains(e.Message.RawText, "we need more rrerr", StringComparison.OrdinalIgnoreCase))
                {
                    Commands.rrerrCount--;
                    if (FiftyLeft)
                        e.Channel.SendMessage("Yeah we do ;)");
                    else
                        e.Channel.SendMessage("We already had " + Commands.rrerrCount + " rrerrs");
                }
                Commands.rrerrCount++;
                File.WriteAllText(@"Commands/rrerr.txt", Convert.ToString(Commands.rrerrCount));
            }
            #endregion
            //Find messages that start with an exclamation point. Not the prettiest code but it'll do.
            if (e.Message.Text.StartsWith("!", StringComparison.OrdinalIgnoreCase) | e.Message.Text.StartsWith("/"))
            {
                switch (e.Message.Text)
                {                    
                    case "!cah":
                        e.Channel.SendMessage(Commands.DoCah(1));
                        break;
                    case "!dhcah":
                        e.Channel.SendMessage(Commands.DoCah(2));
                        break;
                    case "!cahmix":
                        e.Channel.SendMessage(Commands.DoCah(3));
                        break;
                    case "!rrerr ":
                        #region search
                        try
                        {
                            string url = "http://e621.net/post/index.xml?tags=" + e.Message.Text.Replace("!rrerr ", string.Empty) + "%20order:random&limit=1";
                            WebClient client = new WebClient();
                            client.Headers["User-Agent"] = "HaroldChatBot by dds_";
                            client.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                            string data = client.DownloadString(url);
                            int pFrom = data.IndexOf("<id type=\"integer\">") + "<id type=\"integer\">".Length;
                            int pTo = data.LastIndexOf("</id>");
                            String result = data.Substring(pFrom, pTo - pFrom);
                            var pornChannel = e.Server.FindChannels("porn").FirstOrDefault();
                            pornChannel.SendMessage("rrerr: http://e621.net/post/show/" + result);
                        }
                        catch (Exception)
                        {
                            e.Channel.SendMessage("No matches found (or bot dun fukd)");
                        }
                        break;
                    case "/rrerr":
                        try
                        {
                            string url = "https://e621.net/post/index.xml?tags=dragon%20-female%20-mlp%20rating:e%20order:random&limit=1";
                            WebClient client = new WebClient();
                            client.Headers["User-Agent"] = "HaroldChatBot by dds_";
                            client.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                            string data = client.DownloadString(url);
                            int pFrom = data.IndexOf("<id type=\"integer\">") + "<id type=\"integer\">".Length;
                            int pTo = data.LastIndexOf("</id>");
                            String result = data.Substring(pFrom, pTo - pFrom);
                            var pornChannel = e.Server.FindChannels("porn").FirstOrDefault();
                            pornChannel.SendMessage("rrerr: http://e621.net/post/show/" + result);
                        }
                        catch (Exception)
                        {
                            e.Channel.SendMessage("No matches found (or bot dun fukd)");
                        }
                        break;
                    #endregion
                    case "!chat ":
                        if (Commands.isCleverbotToggled == true)
                        {
                            e.Channel.SendMessage(Commands.Cleverbot(e.Message.Text.Replace("!chat ", string.Empty)));
                        }
                        break;
                    case "!wolfram ":
                        if (Commands.isWolframToggled == true)
                        {
                            e.Channel.SendMessage(Commands.Wolfram(e.Message.Text));
                        }
                        break;
                    case "!8ball ":
                        e.Channel.SendMessage(Commands.EightBall());
                        break;
                    case "!music":
                        Commands.Music(e.Message.Text);
                        break;
                }
                if (e.Message.User.ServerPermissions.Administrator)
                {
                    switch (e.Message.Text)

                    {
                        case ("!chat"):
                            Commands.isCleverbotToggled = !Commands.isCleverbotToggled;
                            break;
                        case ("!wolf"):
                            Commands.isWolframToggled = !Commands.isWolframToggled;
                            break;
                        case ("!update"):
                            System.Diagnostics.Process.Start("AccordUpdate.exe");
                            System.Environment.Exit(1);
                            break;
                        case ("!reload"):
                            System.Diagnostics.Process.Start("AccordRestart.exe");
                            System.Environment.Exit(1);
                            break;
                    }

                }
                
                    
                }
        }
    }
}
