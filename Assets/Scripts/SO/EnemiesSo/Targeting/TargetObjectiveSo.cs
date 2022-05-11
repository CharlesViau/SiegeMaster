using Managers;
using UnityEngine;

namespace SO.Enemies.Targeting
{
    [CreateAssetMenu(fileName = "Target Objective", menuName = "ScriptableObjects/Targeting/Target Nexus")]
    public class TargetObjectiveSo : SO.TowerSo.Targeting.TargetingSo
    {
        private Transform _nexus;

        public override void Init(GameObject unit, float range)
        {
            base.Init(unit, range);
            _nexus = NexusManager.Instance.GetTransform;        
        }

        public override Transform GetTheTarget()
        {
            return _nexus;
        }
    }
}
