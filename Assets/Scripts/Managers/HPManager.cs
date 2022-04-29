using System.Collections.Generic;
using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public enum HPType { EnemyHp  }

    public class HPManager : Manager<HP, HPType, HP.Args, HPManager>
    {
        protected override string PrefabLocation => "Prefabs/UI/";    
    }
}