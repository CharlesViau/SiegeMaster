using System.Collections.Generic;
using System.Linq;
using General;
using Units.Types;
using UnityEngine;

public enum CellType { Normal};
public class CellManager : Manager<Cell, CellType, Cell.Args, CellManager>
{
    protected override string PrefabLocation => "Prefabs/Cells/";

    
    public override void Init()
    {
        var hashSet = new HashSet<Cell>(Object.FindObjectsOfType<Cell>().ToList());
        foreach (var item in hashSet)
        {
            Add(item);
        }

        base.Init();
    }

}
