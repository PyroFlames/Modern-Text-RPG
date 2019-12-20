using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public class SceneSequence
    {
        public Renderer.CSSStyle LineStyle { get; private set; }
        public string[] CharacterLine { get; private set; }
        public int PreWaitTime { get; private set; }
        public int PostWaitTime { get; private set; }
        public int TypingSpeed { get; private set; }

        public SceneSequence(Renderer.CSSStyle lineStyle, string[] characterLine, int preWaitTime, int postWaitTime, int typingSpeed)
        {
            LineStyle = lineStyle;
            CharacterLine = characterLine;
            PreWaitTime = preWaitTime;
            PostWaitTime = postWaitTime;
            TypingSpeed = typingSpeed;
        }
    }
}
