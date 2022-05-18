using Units.Interfaces;
using UnityEngine;

namespace Abilities.TargetingSO
{
    [CreateAssetMenu(fileName = "PlayerGroundTargeting", menuName = "ScriptableObjects/TargetingSo/PlayerGroundTargeting")]
    public class PlayerGroundTargeting : TargetingSo
    {
        private Transform _temporaryTransform;

        public override Transform TargetTransform => _temporaryTransform;

        public override void Init(ITargetAcquirer owner, float maxRange)
        {
            base.Init(owner, maxRange);
            _temporaryTransform = new GameObject("tempTransform_targetingSO").transform;
        }

        public override void Refresh()
        {
            if (!Physics.Raycast(Owner.TargetPosition, -Owner.TargetPosition, out var hitDown, MaxRange)) return;
            Vector3 endPoint;
                
            if (Physics.Raycast(Owner.ShootingPosition.position, Owner.AimingDirection, out var aimDirectionHit,
                    MaxRange))
                endPoint = aimDirectionHit.point;
            else
                endPoint = Owner.ShootingPosition.position + Owner.AimingDirection.normalized * MaxRange;

            _temporaryTransform.position = new Vector3(endPoint.x, hitDown.point.y, endPoint.z);
        }
    }
}