using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class PlayerUnitManager : Manager<PlayerUnit, PlayerUnitManager>
    {
        public override void Init()
        {
            foreach (var player in Object.FindObjectsOfType<PlayerUnit>().ToList())
            {
                Add(player);
            }
            
            base.Init();
        }
    }
}
