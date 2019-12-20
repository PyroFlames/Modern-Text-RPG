using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ModernTextRPG
{
    public static class FileManager
    {
        public enum FileType { Item, Weapon, Armor, Consumable, Class, Mob, Boss, Shop, Recipe, Biome }

        private static int FailCounter = 0;

        public static void Init()
        {
            CreateGameFolders();
        }

        public static void LoadCoreFiles()
        {
            List<DataFile> Files = GetFiles(Path.Combine(MainThread.GameFolderPath, "bin"));

            foreach (DataFile file in Files) { LoadFile(file); }
        }

        public static void LoadModFiles()
        {
            //TODO
        }

        public static List<DataFile> GetFiles(string binPath)
        {
            List<DataFile> Files = new List<DataFile>();

            try
            {
                if (!Directory.Exists(binPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(binPath);
                }
                if (!Directory.Exists(Path.Combine(binPath, "files")))
                {
                    DirectoryInfo di = Directory.CreateDirectory(Path.Combine(binPath, "files"));
                }
                if (File.Exists(Path.Combine(binPath, "files", "filelist.xml")))
                {
                    Debug.Log("", false, Debug.LogStates.Standard);
                    Debug.Log("|'Found Core File List'|", false, Debug.LogStates.Standard);

                    XmlReader xReader = XmlReader.Create(Path.Combine(binPath, "files", "filelist.xml"));

                    Debug.Log("|'Collecting Core Game Files'|", false, Debug.LogStates.Standard);

                    try
                    {
                        while (xReader.Read())
                        {
                            if (xReader.NodeType == XmlNodeType.EndElement && xReader.Name.ToString().ToUpper() == "GAMEFILES")
                            {
                                List<DataFile> FilesToRemove = new List<DataFile>();
                                foreach (DataFile file in Files)
                                {
                                    if (File.Exists(Path.Combine(MainThread.GameFolderPath, file.Path)))
                                    {
                                        Debug.Log(Path.Combine(MainThread.GameFolderPath, file.Path).ToString() + " > " + file.Type, false, Debug.LogStates.Standard);
                                    }
                                    else
                                    {
                                        FilesToRemove.Add(file);
                                        Debug.Log(Path.Combine(MainThread.GameFolderPath, file.Path).ToString() + " > " + file.Type, false, Debug.LogStates.Standard);
                                        Debug.Log("^'File Not Found'^", false, Debug.LogStates.Warning);
                                    }
                                }
                                if (FilesToRemove.Count > 0)
                                {
                                    Debug.Log("", false, Debug.LogStates.Standard);
                                    Debug.Log("|'Reloading File List After Modifications'|,", false, Debug.LogStates.Standard);
                                    foreach (DataFile file in FilesToRemove)
                                    {
                                        Files.Remove(file);
                                    }
                                    foreach (DataFile file in Files)
                                    {
                                        if (File.Exists(Path.Combine(MainThread.GameFolderPath, file.Path)))
                                        {
                                            Debug.Log(Path.Combine(MainThread.GameFolderPath, file.Path).ToString() + " > " + file.Type, false, Debug.LogStates.Standard);
                                        }
                                        else
                                        {
                                            Files.Remove(file);
                                            Debug.Log(Path.Combine(MainThread.GameFolderPath, file.Path).ToString() + " > " + file.Type, false, Debug.LogStates.Standard);
                                            Debug.Log(" ^ 'File Not Found' ^ ", false, Debug.LogStates.Warning);
                                        }
                                    }
                                }
                                Debug.Log("|'Collected All Core Files'|", false, Debug.LogStates.Standard);
                            }

                            switch (xReader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    switch (xReader.Name.ToString().ToUpper())
                                    {
                                        case "DATA":
                                            DataFile file = new DataFile(xReader.GetAttribute("path"),
                                                GetFileType(xReader.GetAttribute("type")));
                                            //Debug.Log("FileFound", false, Debug.LogStates.Standard);
                                            Files.Add(file);
                                            break;
                                    }
                                    break;
                            }

                            //xReader.Read();

                        }
                        xReader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.Write("Unrecoverable Loading Error Has Occured.");
                        Console.Write("Press Enter To Exit");
                        Debug.ExceptionCatch(e, Debug.LogStates.Error);
                        Console.Read();
                        Program.Error_Exit();
                    }
                }
                else { throw new Exception("Core File List Doesn't Exist, Line 50, FileInterpreter"); }
            }
            catch (Exception e)
            {
                Console.Write("Error Finding FileData: {0}", e.ToString());
                Debug.Log("Error Finding FileData: " + e.ToString(), false, Debug.LogStates.Error);
            }

            return Files;
        }

        public static void LoadFile(DataFile file)
        {
            FileType ft = file.Type;
            string _path = Path.Combine(MainThread.GameFolderPath, file.Path);
            //string path = @"C:\Users\PyroFlames\AppData\Roaming\.ETF_TextRPG\bin\classlist.xml";

            XmlReader xReader = XmlReader.Create(_path);
            //Console.Clear();

            switch (ft)
            {
                case FileType.Item:
                    LoadItemFile(xReader,Item.ItemType.Material);
                    break;
                case FileType.Armor:
                    LoadItemFile(xReader, Item.ItemType.Armor);
                    break;
                case FileType.Weapon:
                    LoadItemFile(xReader, Item.ItemType.Weapon);
                    break;
                case FileType.Consumable:
                    LoadItemFile(xReader, Item.ItemType.Consumable);
                    break;
                case FileType.Class:
                    LoadClassFile(xReader);
                    break;
                case FileType.Mob:
                    break;
                case FileType.Boss:
                    break;
                case FileType.Shop:
                    break;
                case FileType.Recipe:
                    break;
            }

            /*
            while (xReader.Read())
            {
                //Console.WriteLine(xReader.MoveToNextAttribute());
                switch (xReader.NodeType)
                {
                    case XmlNodeType.Element:
                        //listBox1.Items.Add("<" + xReader.Name + ">");
                        Console.WriteLine("  " + xReader.Name);
                        break;
                    case XmlNodeType.Text:
                        //listBox1.Items.Add(xReader.Value);
                        Console.WriteLine("  " + xReader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        //listBox1.Items.Add("");
                        break;
                }
            }
            */
        }

        private static bool LoadClassFile(XmlReader xReader)
        {
            return false;
        }

        private static void LoadItemFile(XmlReader xReader, Item.ItemType type)
        {

        }



        private static void CreateGameFolders()
        {
            string installFilesPath = "";

            if (!Directory.Exists(MainThread.GameFolderPath))
            {
                bool isValidDirectory = false;
                //DirectoryInfo di = Directory.CreateDirectory(MainThread.GameFolderPath);

                while (isValidDirectory == false)
                {
                    //Renderer.HudOptions[Renderer.HudElements.mainBorder] = true;
                    //Renderer.Update(false);

                    string msg = "Please Enter Your Install Path: ";

                    Console.SetCursorPosition((Console.WindowWidth / 2) - msg.Length / 2, (Console.WindowHeight / 2) - 5);
                    Console.Write(msg);
                    Console.SetCursorPosition((Console.WindowWidth / 2) - (msg.Length / 2) - 1, (Console.WindowHeight / 2) - 4);
                    Console.Write(">");
                    string response = Console.ReadLine();
                    //Renderer.ClearLine(false, (Renderer.WindowWidth / 2) + 16,
                    //    new int[] { (Console.WindowWidth / 2) - (msg.Length / 2), (Console.WindowHeight / 2) - 4 });
                    //Renderer.ClearLine(false, (Renderer.WindowWidth),
                    //    new int[] { 0, (Console.WindowHeight / 2) - 3 });

                    if (Directory.Exists(response))
                    {
                        if (Directory.Exists(Path.Combine(response, "bin")))
                        {
                            if (Directory.Exists(Path.Combine(response, "bin", "files")))
                            {
                                if (File.Exists(Path.Combine(response, "bin", "version.xml")) &&
                                    File.Exists(Path.Combine(response, "bin", "errorlog.xml")))
                                {
                                    if (File.Exists(Path.Combine(response, "bin", "files", "filelist.xml")))
                                    {
                                        isValidDirectory = true;
                                    }
                                }
                            }
                        }
                    }

                    if (isValidDirectory == true)
                    {
                        installFilesPath = response;
                    }
                    else
                    {
                        //Renderer.Update(false);
                        Console.SetCursorPosition((Console.WindowWidth / 2) - (msg.Length / 2) + 5, (Console.WindowHeight / 2) - 2);
                        Console.Write("|'Invalid File Path'|");
                        Program.Sleep(2 * 1000);
                        //Renderer.ClearLine(false, (Renderer.WindowWidth / 2) + 16,
                        //    new int[] { (Console.WindowWidth / 2) - (msg.Length / 2), (Console.WindowHeight / 2) - 2 });
                        //Renderer.ClearLine(false, (Renderer.WindowWidth),
                        //    new int[] { 0, (Console.WindowHeight / 2) - 1 });
                    }
                }

                List<string> directories = new List<string>();
                directories = GetAllSubDirectories(installFilesPath);
                directories.Add(installFilesPath);
                DirectoryInfo di = new DirectoryInfo(installFilesPath);

                List<string> files = new List<string>();
                foreach (string s in directories)
                {
                    string[] f = Directory.GetFiles(s);
                    files.AddRange(f);
                    string x = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        s.Substring((installFilesPath.Length - di.Name.Length), s.Length - installFilesPath.Length + di.Name.Length));
                    if (!Directory.Exists(x))
                    {
                        Directory.CreateDirectory(x);
                    }
                }
                foreach (string s in files)
                {
                    string dest = Path.Combine(MainThread.GameFolderPath,
                        s.Substring(installFilesPath.Length + 1, s.Length - installFilesPath.Length - 1));

                    File.Copy(s, dest);
                }
                //Directory.Move(installFilesPath, 
                //   Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "etftest", "etf"));
                //Console.ReadLine();
            }
        }
        private static List<string> GetAllSubDirectories(string root)
        {
            List<string> dirs = new List<string>();


            string[] directories = Directory.GetDirectories(root);
            foreach (string s in directories)
            {
                dirs.AddRange(GetAllSubDirectories(s));
                dirs.Add(s);
            }

            return dirs;
        }
        public static FileType GetFileType(string type)
        {
            FileType ft = FileType.Item;

            switch (type.ToLower())
            {
                case "item":
                    ft = FileType.Item;
                    break;
                case "weapon":
                    ft = FileType.Weapon;
                    break;
                case "armor":
                    ft = FileType.Armor;
                    break;
                case "consumable":
                    ft = FileType.Consumable;
                    break;
                case "mob":
                    ft = FileType.Mob;
                    break;
                case "boss":
                    ft = FileType.Boss;
                    break;
                case "biome":
                    ft = FileType.Biome;
                    break;
                case "class":
                    ft = FileType.Class;
                    break;
                case "shop":
                    ft = FileType.Shop;
                    break;
                case "recipe":
                    ft = FileType.Recipe;
                    break;
            }

            return ft;
        }
    }
}
