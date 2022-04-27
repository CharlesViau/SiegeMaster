using General;
using Units.Types;
using UnityEngine;

namespace Abilities
{
    public enum AbilityState
    {
        Ready,
        Channeling,
        Active,
        OnCooldown,
    }
    public abstract class AbilitySo : ScriptableObject
    {
        public new string name;
        public string description;
        public float baseCooldown ;
        public float baseCastTime;
        public int manaCost;

        public virtual void Init()
        {
            
        }
        
    }
}
