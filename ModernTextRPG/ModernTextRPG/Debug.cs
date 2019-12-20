using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public static class Debug
    {
        public enum LogStates { Standard, Warning, Error }
        public static bool DebugModeIsActive = false;
        public static string FilePath { get; private set; }

        public static void _Init_(bool IsActive, string logFilePath)
        {
            FilePath = logFilePath;
            DebugModeIsActive = IsActive;
        }

        public static void Clear()
        {
            CreateDebugFile();

            using (StreamWriter sw = File.CreateText(FilePath))
            {
                sw.WriteLine("'File Created At': " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                sw.Close();
            }
        }
        public static void Exit(LogStates state)
        {
            Log("'File Exited At': " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(), false, state);
        }
        public static void Log(string log, bool CheckForCommands, LogStates logstate)
        {
            //if (CheckForCommands == true) { CommandInterpreter.ProcessText(log); }
            if (DebugModeIsActive == false) { return; }

            ProcessDebugLogEntry(log, logstate);
        }

        public static void ExceptionCatch(Exception e, LogStates logstate)
        {
            if (logstate == LogStates.Warning)
            {
                Log(e.ToString(), false, LogStates.Warning);
                Log("Warning Occured At: " + CreateTimeStamp(), false, LogStates.Warning);
            }
            if (logstate == LogStates.Error)
            {
                Log("Error Occured At: " + CreateTimeStamp(), false, LogStates.Error);
                Log(e.ToString(), false, LogStates.Error);
                Log("Unrecoverable Loading Error Has Occured", false, LogStates.Error);
            }
        }

        private static void ProcessDebugLogEntry(string log, LogStates logstate)
        {
            CreateDebugFile();

            switch (logstate)
            {
                case LogStates.Standard:
                    CreateDebugLogEntry(log, logstate);
                    break;
                case LogStates.Warning:
                    //Console.Write("| Warning | " + log);
                    CreateDebugLogEntry("| Warning | " + log, logstate);
                    CreateDebugLogEntry("Warning Occured At: " + CreateTimeStamp(), LogStates.Warning);
                    //Console.ReadLine();
                    break;
                case LogStates.Error:
                    Console.WriteLine("|** ERROR **| " + log);
                    CreateDebugLogEntry("|'** ERROR **'| " + log, logstate);
                    CreateDebugLogEntry("|'** ERROR **'| Error Occured At: " + CreateTimeStamp(), LogStates.Error);
                    Console.ReadLine();
                    break;
            }
        }

        private static void CreateDebugLogEntry(string log, LogStates logstate)
        {
            using (StreamWriter sw = File.AppendText(FilePath))
            {
                sw.WriteLine(log);
                sw.Close();
            }
        }

        private static string CreateTimeStamp()
        {
            string timestamp = DateTime.Now.ToLongDateString();
            timestamp = timestamp + " " + DateTime.Now.ToLongTimeString();

            return timestamp;
        }

        private static void CreateDebugFile()
        {
            try
            {
                /*
                if (!Directory.Exists(PublicGameFolderPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(GameFolder);
                }
                if (!Directory.Exists(PublicCharacterFolderPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(CharacterFolder);
                }
                */
                //string characterFile = Path.Combine(PublicCharacterFolderPath, UserName);
                if (!File.Exists(FilePath))
                {
                    File.CreateText(FilePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Creating User Save File Failed: {0}", e.ToString());
                Console.Read();
            }
        }
    }
}
