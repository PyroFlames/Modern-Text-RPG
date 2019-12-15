using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            Imports.SetWindowPos(consoleWnd, 0, 0, 0, 0, 0, Imports.SWP_NOSIZE | Imports.SWP_NOZORDER);

            Init();
            Debug._Init_(true, Path.Combine(MainThread.GameFolderPath, "bin", "errorlog.xml"));
            //Renderer.Init();
            //MainThread.Init();
            FileManager.Init();
            Debug.Clear();
            //CutSceneManager.Init();
            //CutSceneManager.PlayScene(1);
            //MainThread.MainMenu();
        }
        static void Init()
        {
            Console.Title = "Embers of The Flames RPG";
            Console.SetWindowSize(1, 1);
            //Console.SetBufferSize(220, 83);
            Console.SetBufferSize(Console.LargestWindowWidth - 2, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth - 2, Console.LargestWindowHeight);
            Console.ForegroundColor = ConsoleColor.White;

            //Renderer.WindowHeight = Console.BufferHeight;
            //Renderer.WindowWidth = Console.BufferWidth;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Common");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Uncommon");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Rare");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Epic");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Chaotic");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Legendary");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Mythical");

            Console.ReadLine();
        }
        public static void Exit_Game()
        {
            Debug.Exit(Debug.LogStates.Standard);
            Environment.Exit(0);
        }
        public static void Error_Exit()
        {
            Debug.Exit(Debug.LogStates.Error);
            Environment.Exit(10);
        }

        static class Imports
        {
            public static IntPtr HWND_BOTTOM = (IntPtr)1;
            // public static IntPtr HWND_NOTOPMOST = (IntPtr)-2;
            public static IntPtr HWND_TOP = (IntPtr)0;
            // public static IntPtr HWND_TOPMOST = (IntPtr)-1;

            public static uint SWP_NOSIZE = 1;
            public static uint SWP_NOZORDER = 4;

            [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
            public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, uint wFlags);
        }
    }
}
