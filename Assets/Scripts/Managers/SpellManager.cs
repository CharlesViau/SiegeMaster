using System.Collections.Generic;
using System.Linq;
using BattelObejcts;
using General;
using Units.Types;
using UnityEngine;

namespace Managers
{
    public enum SpellType {Storm,Lightning,Fire,Blood}

    public class SpellManager : Manager<Spell, SpellType, Spell.Args, SpellManager>
    {
        protected override string PrefabLocation => "Prefabs/Spells/";
    }
}