using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class PlayerUnitManager : Manager<PlayerPC, PlayerUnitManager>
    {
        Transform playerUnit;
        public Transform GetTransform { get { return playerUnit; } }
        public override void Init()
        {
            foreach (var player in Object.FindObjectsOfType<PlayerPC>().ToList())
            {
                Add(player);
                playerUnit = player.transform;
            }

            base.Init();
        }
    }
}
