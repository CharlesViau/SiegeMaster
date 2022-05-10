using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Types;
using System.Linq;
using Units.Types.Towers;

public enum TowerType { ArcherTower, Catapult,SeekingTower,FireBall , ArcherTowerPrediction }
public class TowerManager : General.Manager<Tower, TowerType, Tower.Args, TowerManager>
{
    protected override string PrefabLocation => "Prefabs/Towers/";

    public override void Init()
    {
        var hashSet = new HashSet<Tower>(UnityEngine.Object.FindObjectsOfType<Tower>().ToList());
        foreach (var item in hashSet)
        {
            Add(item);
        }

        base.Init();
    }

}


