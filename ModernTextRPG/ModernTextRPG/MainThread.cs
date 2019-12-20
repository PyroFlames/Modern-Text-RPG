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
        public static string GameFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "_ModernTextRPG");

        public static List<CharacterClass> ClassList = new List<CharacterClass>();
        public static List<Recipe> RecipeList = new List<Recipe>();
        public static List<Biome> BiomeList = new List<Biome>();
        public static List<Item> ItemList = new List<Item>();
        public static List<Shop> ShopList = new List<Shop>();
        public static List<Mob> MobList = new List<Mob>();

        public static Dictionary<string, CharacterClass> ClassDictionary = new Dictionary<string, CharacterClass>();
        public static Dictionary<string, Recipe> RecipeDictionary = new Dictionary<string, Recipe>();
        public static Dictionary<string, Biome> BiomeDictionary = new Dictionary<string, Biome>();
        public static Dictionary<string, Item> ItemDictionary = new Dictionary<string, Item>();
        public static Dictionary<string, Shop> ShopDictionary = new Dictionary<string, Shop>();
        public static Dictionary<string, Mob> MobDictionary = new Dictionary<string, Mob>();

        public static void Init()
        {
            Renderer.HudOptions[Renderer.HudElements.MainBorder] = true;
            Renderer.HudOptions[Renderer.HudElements.HudHeader] = true;
            Renderer.HudOptions[Renderer.HudElements.HudFooter] = true;
            Renderer.HudOptions[Renderer.HudElements.SideMenu1] = true;
            Renderer.HudOptions[Renderer.HudElements.SideMenu2] = true;
            Renderer.HudOptions[Renderer.HudElements.MainGameBorder] = true;
            //
            Renderer.Update();

            CutSceneManager.DisplayScene("splash_screen");

            //Console.ReadLine();
        }

        public static void MainMenu()
        {
            ConsoleKey input = ConsoleKey.NumPad0;

            while (input != ConsoleKey.MediaStop)
            {
                input = Console.ReadKey(true).Key;
                Program.Sleep(5);
                Renderer.Update();
            }

            ControlTest();
        }

        public static void StartGame()
        {

        }

        public static void CreateNewGame()
        {
            Debug.Log("'Menu Cycle': New Game Menu", false, Debug.LogStates.Standard);
        }

        public static void LoadGame()
        {
            Debug.Log("'Menu Cycle': Load Game Menu", false, Debug.LogStates.Standard);
        }

        public static void ControlTest()
        {
            for (int i = 0; i < 100; i++)
            {
                //Console.Clear();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.W:
                        Console.Write("__UP__");
                        break;
                    case ConsoleKey.S:
                        Console.Write("__DOWN__");
                        break;
                    case ConsoleKey.A:
                        Console.Write("__LEFT__");
                        break;
                    case ConsoleKey.D:
                        Console.Write("__RIGHT__");
                        break;
                    case ConsoleKey.E:
                        Console.Write("__INTERACT__");
                        break;
                    case ConsoleKey.Escape:
                        Console.Write("__MENU__");
                        break;
                }
                //Program.Sleep(2500);
            }
        }

        public static void test()
        {
            //Item a = new Item();
            //a.ItemEffect.InvokeEffect();
        }
    }
}
