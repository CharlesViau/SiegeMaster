using System;
using Abilities.AbilitySO;
using Abilities.TargetingSO;
using Managers;
using UnityEngine;


namespace Abilities.TargetingStateSO
{
    public abstract class AbilityTargetingStateSo : ScriptableObject
    {
        public TargetingSo targetingSo;
        protected AbilitySo AbilitySo;
        [HideInInspector] public SpellUIType spellUIType;

        public Action OnFirePress;
        public Action OnFireRelease;

        public virtual void Init(AbilitySo abilitySo)
        {
            AbilitySo = abilitySo;
            OnFirePress = OnFirePressEvent;
            OnFireRelease = OnFireReleaseEvent;

            Instantiate(targetingSo);
            targetingSo.Init(abilitySo, abilitySo.stats.maxRange);
        }

        public abstract void Refresh();

        public abstract void OnEnter();

        public abstract void OnExit();

        protected abstract void OnFirePressEvent();
        protected abstract void OnFireReleaseEvent();
    }
}