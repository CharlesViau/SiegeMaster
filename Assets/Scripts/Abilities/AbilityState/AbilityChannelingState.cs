using System;
using Abilities.AbilitySO;
using General;
using UnityEngine;

namespace Abilities.AbilityState
{
    public class AbilityChannelingState : IState
    {
        private readonly AbilitySo _abilitySo;
        private float _channelTime;
        public bool HasCompleted => _channelTime >= _abilitySo.stats.baseChannelTime;
        public bool HasBeenInterrupt { get; set; }

        public AbilityChannelingState(AbilitySo abilitySo)
        {
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
            //TODO : Do something on cast fail
        }
    }
}