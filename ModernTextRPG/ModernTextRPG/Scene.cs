using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public class Scene
    {
        public string Name { get; private set; }
        public bool IsFullScreen { get; private set; }
        private List<SceneSequence> SequenceList = new List<SceneSequence>();

        public Scene(string name, bool fullscreen, List<SceneSequence> sequences)
        {
            Name = name;
            IsFullScreen = fullscreen;
            SequenceList = sequences;
        }

        public void PlayScene()
        {
            foreach (SceneSequence ss in SequenceList)
            {
                switch (ss.LineStyle)
                {
                    case Renderer.CSSStyle.Left:

                        break;
                    case Renderer.CSSStyle.Center:
                        
                        break;
                    case Renderer.CSSStyle.Right:

                        break;
                    case Renderer.CSSStyle.Other:

                        break;
                }
                Program.Sleep(ss.PreWaitTime);
                for (int i = 0; i < ss.CharacterLine.Length; i++)
                {
                    Console.Write(ss.CharacterLine[i]);
                    Program.Sleep(ss.TypingSpeed);
                }

                Program.Sleep(ss.PostWaitTime);
            }
        }
    }
}
