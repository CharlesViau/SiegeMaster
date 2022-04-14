using System.Collections.Generic;
using System.Linq;
using General;
using Units.Types;

namespace Managers
{
    public enum EnemyType { Archer, Sneaky }

    public class EnemyManager : Manager<Enemy, EnemyType, Enemy.Args, EnemyManager>
    {
        protected override string PrefabLocation => "Prefabs/Enemies/";

        public override void Init()
        {
            var hashSet = new HashSet<Enemy>(UnityEngine.Object.FindObjectsOfType<Enemy>().ToList());
            foreach (var item in hashSet)
            {
                Add(item);
            }

            base.Init();    
        }

    }
}