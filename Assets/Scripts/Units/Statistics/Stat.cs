using System;
using UnityEngine;

namespace Units.Statistics
{
    [Serializable]
    public abstract class Stat
    {
        public float Current { get; set; }

        [SerializeField] protected float baseValue;

        public virtual void Init()
        {
            Current = baseValue;
        }
    }
    [Serializable]
    public class Regeneration : Stat
    {
        [SerializeField] public float timeInterval;
        private float _currentTime;
        private Stat _stat;

        public void InitRegen(Stat stat)
        {
            _stat = stat;
        }
        public override void Init()
        {
            base.Init();
            _currentTime = timeInterval;
        }

        public void Refresh()
        {
            _currentTime -= Time.deltaTime;

            if (!(_currentTime <= 0)) return;
            _stat.Current += baseValue;
            _currentTime = timeInterval;
        }
    }
    [Serializable]
    public class Health : Stat
    {
        public Regeneration regeneration;

        public override void Init()
        {
            base.Init();
            regeneration.InitRegen(this);
            regeneration.Init();
        }

        public void Refresh()
        {
            regeneration.Refresh();
        }
    }

    [Serializable]
    public class Mana : Stat
    {
        public Regeneration regeneration;

        public override void Init()
        {
            base.Init();
            regeneration.InitRegen(this);
            regeneration.Init();
        }

        public void Refresh()
        {
            regeneration.Refresh();
        }
    }
}