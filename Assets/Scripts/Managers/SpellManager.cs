using System.Collections.Generic;
using System.Linq;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public enum SpellType { EnemyHp, NexusHp }

    public class SpellManager : Manager<Spell, SpellType, Spell.Args, SpellManager>
    {
        protected override string PrefabLocation => "Prefabs/Spells/";
    }
}