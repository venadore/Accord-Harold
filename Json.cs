using Newtonsoft.Json;
using Accord;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accord
{
    //The Json class. For parsing json. Because json is cool.

    public static class Json
    {
        //Load the json and make sure the file isn't broken
        //First create dynamics
        public static dynamic settings;
        public static dynamic displayName;
        public static dynamic token;
        //Then actually load the json
        public static void LoadJson()
        {
            try
            {              
                settings = JsonConvert.DeserializeObject(File.ReadAllText("settings.json"));
                //Account stuff
                //First read from the json
                string _displayName = (string)settings.Account.DisplayName;
                string _token = (string)settings.Account.Token;               
                //Let's make it so that we can grab these values from anywhere
                displayName = _displayName;
                token = _token;
                //And finally tell the user everything is ok
                Console.WriteLine("JSON loaded!");
            }
            //And if someone messed up, tell them.
            catch (Exception)
            {   
                Console.WriteLine("Failed to load JSON! Make sure your formatting is correct.");
                return;
            }
           
        }
            
        }       

    }
