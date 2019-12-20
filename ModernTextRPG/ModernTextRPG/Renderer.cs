using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public static class Renderer
    {
        public enum CSSStyle { Left, Center, Right, Other }
        public enum HudElements
        {
            MainBorder, HudHeader, HudFooter, SideMenu1, SideMenu2, PlayerInfo, MainGameBorder, Crosshair, Inventory, Stats
        }

        private enum GameAreaCordLabel
        {
            TopLeft, TopRight, BottomLeft, BottomRight, Center, LeftCenter, RightCenter, TopCenter, BottomCenter
        }
        private static Dictionary<GameAreaCordLabel, int[]> GameAreaCords = new Dictionary<GameAreaCordLabel, int[]>();

        public static Dictionary<HudElements, bool> HudOptions;
        public static int CursorPositionLeft = 5, CursorPositionTop = 5;
        public static int WindowWidth, WindowHeight;

        private static int FailCounter = 0;

        private static string[] SelectionCharacters = new string[2] { "<", ">" };
        private const ConsoleColor DefaultConsoleForegroundColor = ConsoleColor.White;
        private const ConsoleColor DefaultConsoleBackgroundColor = ConsoleColor.Black;

        public static void Init()
        {
            HudOptions = new Dictionary<HudElements, bool>();
            WindowWidth = Console.BufferWidth;
            WindowHeight = Console.BufferHeight;

            GameAreaCords[GameAreaCordLabel.TopLeft] = new int[] { 5, 12 };
            GameAreaCords[GameAreaCordLabel.TopRight] = new int[] { 154, 12 };
            GameAreaCords[GameAreaCordLabel.BottomLeft] = new int[] { 5, 61 };
            GameAreaCords[GameAreaCordLabel.BottomRight] = new int[] { 154, 61 };
            GameAreaCords[GameAreaCordLabel.LeftCenter] = new int[] { 5, ((62 - 11) / 2) + 11 };
            GameAreaCords[GameAreaCordLabel.RightCenter] = new int[] { 154, ((62 - 11) / 2) + 11 };
            GameAreaCords[GameAreaCordLabel.TopCenter] = new int[] { ((155 - 4) / 2) + 4, 12 };
            GameAreaCords[GameAreaCordLabel.BottomCenter] = new int[] { ((155 - 4) / 2) + 4, 61 };
            GameAreaCords[GameAreaCordLabel.Center] = new int[] { ((155 - 4) / 2) + 4, ((62 - 11) / 2) + 11 };
        }

        public static void Update()
        {
            Console.Clear();

            try
            {
                for (int i = 0; i < HudOptions.Count; i++)
                {
                    if (HudOptions.ElementAt(i).Value == true)
                    {
                        if (DisplayHudItem(HudOptions.ElementAt(i).Key)) { }
                        else { }
                    }
                }

                //Console.SetCursorPosition(CursorPositionLeft, CursorPositionTop);
                FailCounter = 0;
            }
            catch (Exception e)
            {
                FailCounter++;
                Console.Write("ERROR: {0}", e.ToString());
                if (FailCounter > 1)
                {
                    Console.Write("Unrecoverable Rendering Error Has Occured.");
                    Console.Write("Press Enter To Exit");
                    Debug.ExceptionCatch(e, Debug.LogStates.Error);
                    Console.Read();
                    Program.Error_Exit();

                }
                else { Debug.ExceptionCatch(e, Debug.LogStates.Warning); Update(); }
            }
        }

        private static bool DisplayHudItem(HudElements HudItem)
        {
            bool Passed = false;
            switch (HudItem)
            {
                /*
                case HudElements.splashScreen:
                    DisplayHudItem(HudElements.mainBorder);
                    Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize("Embers of The Flames RPG"), GetScreenCenterHeight() - 8);
                    WriteColor(ConsoleColor.Red, DefaultConsoleBackgroundColor, "Embers of The Flames RPG", true);
                    Thread.Sleep(1 * 250);
                    Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize("Created By PyroFlames"), GetScreenCenterHeight() - 7);
                    WriteColor(ConsoleColor.DarkRed, DefaultConsoleBackgroundColor, "Created By PyroFlames", true);
                    Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize(">                  "), GetScreenCenterHeight() - 5);
                    Console.Write(">");
                    break;
                case HudElements.mainMenu:
                    DisplayHudItem(HudElements.mainBorder);
                    DisplayHudItem(HudElements.hudHeader);
                    DisplayHudItem(HudElements.hudfooter);
                    Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize("Embers of The Flames RPG"), 2);
                    WriteColor(ConsoleColor.Red, DefaultConsoleBackgroundColor, "Embers of The Flames RPG", true);

                    CreateMenuList(new string[] { "New Game", "Load Game", "Options", "Update", "Exit" }, 1, new int[] { 0, 10 }, true, false, true);

                    SetTypingPos();
                    CreateTypingZone(">", ConsoleColor.White, ConsoleColor.White);
                    break;
                case HudElements.optionsMenu:
                    DisplayHudItem(HudElements.mainBorder);
                    DisplayHudItem(HudElements.hudHeader);
                    DisplayHudItem(HudElements.hudfooter);
                    Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize("Embers of The Flames RPG"), 2);
                    WriteColor(ConsoleColor.Red, DefaultConsoleBackgroundColor, "Embers of The Flames RPG", true);

                    CreateMenuList(new string[] { "Typing Color", "Text Speed", "Music", "Volume", "Back" }, 1, new int[] { 0, 10 }, true, false, true);

                    SetTypingPos();
                    CreateTypingZone(">", ConsoleColor.White, ConsoleColor.White);
                    break;
                    */
                case HudElements.Crosshair:
                    DrawLine(true, 50, "|", GameAreaCords[GameAreaCordLabel.TopCenter]);
                    DrawLine(false, 150, "-", GameAreaCords[GameAreaCordLabel.LeftCenter]);
                    break;
                case HudElements.MainGameBorder:
                    DrawLine(true, 51, "|", new int[] { 4, 11 });
                    DrawLine(true, 51, "|", new int[] { 155, 11 });
                    DrawLine(false, 152, "-", new int[] { 4, 11 });
                    DrawLine(false, 152, "-", new int[] { 4, 62 });
                    break;
                case HudElements.MainBorder:
                    DrawLine(true, WindowHeight - 2, "|", new int[] { 0, 1 });
                    DrawLine(true, WindowHeight - 2, "|", new int[] { WindowWidth - 1, 1 });
                    DrawLine(false, WindowWidth, "-", new int[] { 0, 0 });
                    DrawLine(false, WindowWidth, "-", new int[] { 0, WindowHeight - 2 });
                    break;
                case HudElements.HudHeader:
                    DrawLine(false, WindowWidth - 2, "-", new int[] { 1, 4 });
                    break;
                case HudElements.HudFooter:
                    DrawLine(false, WindowWidth - 2, "-", new int[] { 1, WindowHeight - 6 });
                    break;
                case HudElements.SideMenu1:
                    DrawLine(true, WindowHeight - 11, "|", new int[] { WindowWidth - 40, 5 });
                    DrawLine(true, 3, "|", new int[] { WindowWidth - 40, 1 });
                    break;
                case HudElements.SideMenu2:
                    DrawLine(true, WindowHeight - 11, "|", new int[] { WindowWidth - 40, 5 });
                    DrawLine(true, WindowHeight - 11, "|", new int[] { WindowWidth - 80, 5 });
                    DrawLine(true, 3, "|", new int[] { WindowWidth - 80, 1 });
                    break;
                case HudElements.Stats:
                    break;
                case HudElements.Inventory:
                    break;
                case HudElements.PlayerInfo:
                    break;
            }

            return Passed;
        }

        private static void DrawLine(bool IsVertical, int size, string character, int[] cursorStartPos)
        {
            Console.SetCursorPosition(cursorStartPos[0], cursorStartPos[1]);

            if (IsVertical == true)
            {
                for (int i = 0; i < size; i++)
                {
                    Console.SetCursorPosition(cursorStartPos[0], cursorStartPos[1] + i);
                    Console.Write(character);
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    Console.Write(character);
                }
            }
        }

        public static void ClearLine(bool IsVertical, int size, int[] cursorStartPos)
        {
            DrawLine(IsVertical, size, " ", cursorStartPos);
        }

        public static void WriteColor(ConsoleColor ForegroundColor, ConsoleColor BackgroundColor, string character, bool ReturnToDefault)
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;

            Console.Write(character);

            if (ReturnToDefault == true)
            {
                Console.ForegroundColor = DefaultConsoleForegroundColor;
                Console.BackgroundColor = DefaultConsoleBackgroundColor;
            }
        }

        public static void CreateTypingZone(string typingCharacter, ConsoleColor PlayerTypingColor, ConsoleColor prefixColor)
        {
            Console.ForegroundColor = prefixColor;
            Console.BackgroundColor = DefaultConsoleBackgroundColor;

            Console.Write(typingCharacter + " ");

            Console.ForegroundColor = PlayerTypingColor;
        }

        public static void CreateMenuList(string[] elements, int BaseIndex, int[] StartingPos, bool CenteredWidth, bool CenteredHeight, bool drawOutline)
        {
            try
            {
                Console.SetCursorPosition(StartingPos[0], StartingPos[1]);

                string IndexSpacing = "    ";
                if (elements.Count() > 9) { IndexSpacing += " "; }
                if (elements.Count() > 99) { IndexSpacing += " "; }

                string LongestLength = "";
                if (CenteredWidth == true || drawOutline == true)
                {
                    foreach (string s in elements)
                    {
                        if (s.Length > LongestLength.Length) { LongestLength = s; }
                    }
                }
                LongestLength += IndexSpacing;

                if (CenteredWidth == true)
                {
                    if (CenteredHeight == true)
                    {
                        Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize(LongestLength), GetScreenCenterHeight());
                    }
                    else
                    {
                        Console.SetCursorPosition(GetScreenCenterWidthBasedOnTextSize(LongestLength), StartingPos[1]);
                    }
                }
                else if (CenteredHeight == true && CenteredWidth == false)
                {
                    Console.SetCursorPosition(StartingPos[0], GetScreenCenterHeight());
                }

                if (drawOutline == true) { DrawOutline(LongestLength.Length, elements.Count(), 1); }

                int x = 0, y = 0;
                x = Console.CursorLeft;
                y = Console.CursorTop;

                for (int i = 0; i < elements.Count(); i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    DrawSelectionIndex(BaseIndex + i, ConsoleColor.Green);
                    Console.Write(elements[i]);
                }
            }
            catch (Exception e)
            {
                FailCounter++;
                Console.Write("ERROR: {0}", e.ToString());
                if (FailCounter > 1)
                {
                    Console.Write("Unrecoverable Rendering Error Has Occured.");
                    Console.Write("Error Creating Menu List.");
                    Console.Write("Press Enter To Exit");
                    Debug.Log("Unrecoverable Rendering Error Has Occured.", false, Debug.LogStates.Error);
                    Debug.Log("Error Creating Menu List.", false, Debug.LogStates.Error);
                    Debug.ExceptionCatch(e, Debug.LogStates.Error);
                    Console.Read();
                    Program.Error_Exit();

                }
                else { Debug.ExceptionCatch(e, Debug.LogStates.Warning); Update(); }
            }
        }

        public static int GetScreenCenterWidthBasedOnTextSize(string text)
        {
            int pos = 0;

            pos = Convert.ToInt32((WindowWidth / 2) - (text.Length / 2));

            return pos;
        }
        public static int GetScreenCenterHeight()
        {
            int pos = 0;

            pos = Convert.ToInt32(WindowHeight / 2);

            return pos;
        }
        public static int GetScreenCenterWidth()
        {
            int pos = 0;

            pos = Convert.ToInt32(WindowWidth / 2);

            return pos;
        }
        private static int GetTypingPos(bool IsWidth)
        {
            int x = 2;
            int y = WindowHeight - 4;

            if (IsWidth == true) { return x; }
            else { return y; }
        }

        public static void SetTypingPos()
        {
            Console.SetCursorPosition(GetTypingPos(true), GetTypingPos(false));
        }

        public static void DrawOutline(int MaxWidth, int NumofRows, int Spacing)
        {
            Spacing++;
            MaxWidth--;
            NumofRows--;
            int x = Console.CursorLeft;
            int y = Console.CursorTop;

            DrawLine(true, NumofRows + (Spacing * 2), "|", new int[] { x - Spacing, y - Spacing });
            DrawLine(true, NumofRows + (Spacing * 2), "|", new int[] { x + MaxWidth + Spacing, y - Spacing });
            DrawLine(false, MaxWidth + (Spacing * 2) + 1, "-", new int[] { x - Spacing, y - Spacing });
            DrawLine(false, MaxWidth + (Spacing * 2) + 1, "-", new int[] { x - Spacing, y + NumofRows + Spacing });

            Console.SetCursorPosition(x, y);
        }

        public static void DrawSelectionIndex(int index, ConsoleColor indexColor, string[] surroundingCharacters)
        {
            Console.ForegroundColor = DefaultConsoleForegroundColor;
            Console.BackgroundColor = DefaultConsoleBackgroundColor;
            Console.Write(surroundingCharacters[0]);

            Console.ForegroundColor = indexColor;
            Console.Write(index.ToString());

            Console.ForegroundColor = DefaultConsoleForegroundColor;
            Console.BackgroundColor = DefaultConsoleBackgroundColor;
            Console.Write(surroundingCharacters[1]);
            Console.Write(" ");
        }
        public static void DrawSelectionIndex(int index, ConsoleColor indexColor)
        {
            Console.ForegroundColor = DefaultConsoleForegroundColor;
            Console.BackgroundColor = DefaultConsoleBackgroundColor;
            Console.Write(SelectionCharacters[0]);

            Console.ForegroundColor = indexColor;
            Console.Write(index.ToString());

            Console.ForegroundColor = DefaultConsoleForegroundColor;
            Console.BackgroundColor = DefaultConsoleBackgroundColor;
            Console.Write(SelectionCharacters[1]);
            Console.Write(" ");
        }

        #region GameSpace Region
        public enum GameSpacePositions { Top, Left, Center }
        public static int[] GetGameSpacePosition(GameSpacePositions[] positions)
        {
            int[] pos = null;



            /*
            foreach(GameSpacePositions gsp in positions)
            {
                switch (gsp)
                {

                }
            }
            */

            return pos;
        }
        #endregion
    }
}
