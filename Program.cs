//Accord, Developed by Venadore.
//No license or TOS, just credit me for the base code and that's it.
using Newtonsoft.Json;
using Accord;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatterBotAPI;
using WolframAlphaNET;

namespace Accord
{
    //If you want basic commands, there's no need to mess with this.
    class Program
    {
        public static ChatterBotSession _cleverbot;
        public static WolframAlpha _wolfram;
        static void LoadCleverbot()
        {
            try
            {
                ChatterBotFactory factory = new ChatterBotFactory();
                ChatterBot bot1 = factory.Create(ChatterBotType.CLEVERBOT);
                ChatterBotSession bot1session = bot1.CreateSession();
                _cleverbot = bot1session;
                Console.WriteLine("Cleverbot API Loaded!");
            }
            catch (Exception)
            {
                Console.WriteLine("Could not load Cleverbot API");
            }
        }
        static void Main(string[] args)
        {
           // WolframAlpha wolfram = new WolframAlpha("LR4VLQ-AT84QA3Y3T");
            //_wolfram = wolfram;
            //Load DisplayName and token
            Json.LoadJson();
            //Run Cleverbot
           // LoadCleverbot();
            //Then actually run the bot
            Bot.RunBot();
        }
    }    
}
