using UnityEngine;

namespace Abilities.TargetingSO
{
    public class PlayerGroundLocationTargeting : TargetingSo
    {
        public override void Refresh()
        {
            if (!Physics.Raycast(TargetTransform.position, -TargetTransform.up, out var hitDown, MaxRange)) return;
            Vector3 endPoint;
                
            if (Physics.Raycast(Owner.ShootingPosition.position, Owner.AimingDirection, out var aimDirectionHit,
                    MaxRange))
                endPoint = aimDirectionHit.point;
            else
                endPoint = Owner.ShootingPosition.position + Owner.AimingDirection.normalized * MaxRange;

            TargetTransform.position = new Vector3(endPoint.x, hitDown.point.y, endPoint.z);
        }
    }
}