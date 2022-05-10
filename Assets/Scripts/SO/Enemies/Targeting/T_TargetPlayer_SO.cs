using Managers;
using SO.TowerSo.Targeting;
using UnityEngine;

namespace SO.Enemies.Targeting
{
    [CreateAssetMenu(fileName = "Target Player", menuName = "ScriptableObjects/Targeting/Target Player")]
    public class TargetPlayerSo : TargetingSo
    {
        private Transform _nexus;
        private Transform _player;

        public override void Init(GameObject unit, float range)
        {
            base.Init(unit, range);
            _nexus = NexusManager.Instance.GetTransform;
            _player = PlayerUnitManager.Instance.GetTransform;
        }

        public override Transform GetTheTarget()
        {
            return DetectPlayer() ? _player : _nexus;
        }

        private bool DetectPlayer()
        {
            if (_player == null) return false;
            return Vector3.Distance(Owner.transform.position, _player.position) < MaxRange;
        }
    }
}