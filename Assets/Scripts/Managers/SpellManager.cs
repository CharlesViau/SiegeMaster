using BatteObjects;
using General;

namespace Managers
{
    public enum SpellType {Storm,Lightning,Fire,Explositon}

    public class SpellManager : Manager<Spell, SpellType, Spell.Args, SpellManager>
    {
        protected override string PrefabLocation => "Prefabs/Spells/";
    }
}