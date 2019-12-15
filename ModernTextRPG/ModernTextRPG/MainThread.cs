using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public static class MainThread
    {
        public static string GameFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".ModernTextRPG");


        public static List<Item> ItemList = new List<Item>();

        public static Dictionary<string, Item> ItemDictionary = new Dictionary<string, Item>();

        public static void CreateNewGame()
        {
            Debug.Log("'Menu Cycle': New Game Menu", false, Debug.LogStates.Standard);
        }

        public static void LoadGame()
        {
            Debug.Log("'Menu Cycle': Load Game Menu", false, Debug.LogStates.Standard);
        }

        public static void test()
        {
            Item a = new Item();
            a.ItemEffect.InvokeEffect();
        }
    }
}
