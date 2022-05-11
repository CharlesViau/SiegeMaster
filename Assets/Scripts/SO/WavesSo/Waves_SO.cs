using General;
using Managers;
using UnityEngine;
using Units.Types;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves")]
public class Waves_SO : ScriptableObject
{
    public int nbOfArcherEnemies;
    public int nbOfSneakyEnemies;
    public int nbOfWarriorEnemies;
    [HideInInspector]
    public int NbToSpawnPerWave { get { return nbOfWarriorEnemies + nbOfSneakyEnemies + nbOfArcherEnemies; ; } }
}
