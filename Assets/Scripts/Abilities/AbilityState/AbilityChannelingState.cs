using System;
using Abilities.SO;
using General;
using UnityEngine;

namespace Abilities.AbilityState
{
    public class AbilityChannelingState : IState
    {
        private readonly AbilitySo _abilitySo;
        private readonly Action _onCast;
        private float _channelTime;
        public bool HasCompleted => _channelTime >= _abilitySo.baseChannelTime;
        public bool HasBeenInterrupt { get; set; }

        public AbilityChannelingState(AbilitySo abilitySo, Action onCast)
        {
            _onCast = onCast;
            _abilitySo = abilitySo;
        }
        public void Refresh()
        {
            _channelTime += Time.deltaTime;
        }

        public void OnEnter()
        {
            _channelTime = 0;
            HasBeenInterrupt = false;
        }

        public void OnExit()
        {
            if (HasCompleted)
            {
                _onCast.Invoke();
            }
            else
            {
                //TODO : Do something on cast fail
            }
        }
    }
}