using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernTextRPG
{
    public class Effect
    {
        public enum EffectType { Healing, Mana, Attack, MagicAttack, Armor, MagicArmor, Health, CritChance }
        public enum EffectTarget { Self, SingleMob, AllMobs, All }
        public enum EffectState { Instant, Lasting, WhileEquiped }

        public List<EffectType> effectTypes = new List<EffectType>();
        public List<EffectTarget> effectTargets = new List<EffectTarget>();
        public List<EffectState> effectStates = new List<EffectState>();
        public List<int> effectDurations = new List<int>();

        public Effect(List<EffectType> effecttype, List<EffectTarget> effecttarget, List<EffectState> effectstate, List<int> effectduration)
        {
            effectTypes = effecttype;
            effectTargets = effecttarget;
            effectStates = effectstate;
            effectDurations = effectduration;
        }

        public void InvokeEffect()
        {

        }
    }
}
