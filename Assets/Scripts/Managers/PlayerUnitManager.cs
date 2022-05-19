using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class PlayerUnitManager : Manager<PlayerUnit, PlayerUnitManager>
    {
        Transform playerUnit;
        public Transform GetTransform { get { return playerUnit; } }
        public override void Init()
        {
            foreach (var player in Object.FindObjectsOfType<PlayerUnit>().ToList())
            {
                Add(player);
                playerUnit = player.transform;
            }

            base.Init();
        }
    }
}
