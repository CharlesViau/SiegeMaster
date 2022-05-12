using General;
using Managers;
using UnityEngine;
using Units.Types;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Waves")]
public class Waves_SO : ScriptableObject
{
    [SerializeField] int nbOfArcherEnemies;
    [SerializeField] int nbOfSneakyEnemies;
    [SerializeField] int nbOfWarriorEnemies;
    public int NbOfArcherEnemies { get { return nbOfArcherEnemies; } }
    public int NbOfSneakyEnemies { get { return nbOfSneakyEnemies; } }
    public int NbOfWarriorEnemies { get { return nbOfWarriorEnemies; } }
    public int NbToSpawnPerWave { get { return nbOfWarriorEnemies + nbOfSneakyEnemies + nbOfArcherEnemies; ; } }
}
