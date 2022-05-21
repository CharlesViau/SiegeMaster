using System;
using Units.Interfaces;
using Units.Statistics;
using UnityEngine;

namespace Units.Types
{

    public abstract class PlayerUnit : Unit,ICameraController
    {
        public Action OnRespawn;
        protected abstract override Vector3 AimedPosition { get; }

        public Stats stats;

        public override void Init()
        {
            base.Init();
            OnRespawn = OnRespawnEvent;
            stats.Init(this);
        }

        public override void Refresh()
        {
            base.Refresh();
            
            if(!IsDead)
                stats.Refresh();
            else
            {
                //TODO: Respawn Timer
            }
        }

        public override void GotShot(float damage)
        {
            stats.health.Current -= damage;
            Debug.Log("Player got damage, current health : " + stats.health.Current);
        }

        protected override void OnDeathEvent()
        {
            //TODO: trigger death animation, Put a countdown to Respawn player.
            Debug.Log("IsDead");
        }

        protected void OnRespawnEvent()
        {
            //TODO: Reset HP and Mana
        }

        public abstract void Look();

    }
}