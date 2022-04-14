using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public class HeroManager : Manager<Hero, HeroManager>
    {
        public override void Init()
        {
            foreach (var player in Object.FindObjectsOfType<Hero>().ToList())
            {
                Add(player);
            }
            
            base.Init();
        }
    }
}
