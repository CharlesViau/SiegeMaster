using System;
using General;
using UnityEngine;

namespace Units.Statistics
{
    public abstract class Stat
    {
        protected float current;
        [SerializeField] 
        private readonly float baseValue;
        protected float growthPerLevel; 
        public float Maximum => baseValue + bonus;
        protected float bonus;
        

        public virtual void Refresh() { }
    }

    public class HealthRegen : Stat
    {
        public float Current => current;
        
    }

    public class Health : Stat
    {
        public float Current => current;
        public HealthRegen HealthRegen;
        
    }
}