using General;
using UnityEngine;

namespace Abilities.AbilityState
{
    public class AbilityChannelingState : State
    {
        public float ChannelTime { get; private set; }
        public override void Refresh()
        {
            ChannelTime += Time.deltaTime;
        }

        public override void OnEnter()
        {
            ChannelTime = 0;
        }

        public override void OnExit()
        {
           
        }
    }
}