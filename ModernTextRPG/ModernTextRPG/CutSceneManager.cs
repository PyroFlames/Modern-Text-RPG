using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public static class CutSceneManager
    {
        public static Dictionary<string, Scene> SceneDictionary = new Dictionary<string, Scene>();

        public static void Init()
        {

        }

        public static void DisplayScene(string scene)
        {
            try
            {
                SceneDictionary[scene].PlayScene();
            }
            catch(Exception e)
            {
                Debug.ExceptionCatch(e, Debug.LogStates.Warning);
            }
        }
    }
}
