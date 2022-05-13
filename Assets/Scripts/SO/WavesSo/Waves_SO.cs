using General;
using Managers;
using UnityEngine;
using Units.Types;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave")]
public class Waves_SO : ScriptableObject
{
    #region Fields
    #region Private
    Transform parentObject;
    Transform[] spawnPositions;
    int randomPosition;
    #endregion

    #region Inspector
    [SerializeField] int nbOfArcherEnemies;
    [SerializeField] int nbOfSneakyEnemies;
    [SerializeField] int nbOfWarriorEnemies;
    #endregion

    #region public
    public int NbOfArcherEnemies { get { return nbOfArcherEnemies; } }
    public int NbOfSneakyEnemies { get { return nbOfSneakyEnemies; } }
    public int NbOfWarriorEnemies { get { return nbOfWarriorEnemies; } }
    public int NbToSpawnPerWave { get { return nbOfWarriorEnemies + nbOfSneakyEnemies + nbOfArcherEnemies; ; } }
    #endregion
    #endregion

    #region Methods
    #region Public
    public void Init(Transform _parent, Transform[] _spawnPositions)
    {
        parentObject = _parent;
        spawnPositions = _spawnPositions;
    }
    public void CreateEnemies()
    {
        SpawnEnemies(EnemyType.ArcherEnemy, NbOfArcherEnemies);
        SpawnEnemies(EnemyType.SneakyEnemy, NbOfSneakyEnemies);
        SpawnEnemies(EnemyType.WarriorEnemy, NbOfWarriorEnemies);
    }
    #endregion

    #region private
    void SpawnEnemies(EnemyType enemyType, int nbToSpawn)
    {
        for (int i = 0; i < nbToSpawn; i++)
        {
            randomPosition = Random.Range(0, spawnPositions.Length);
            EnemyManager.Instance.Create(enemyType,
                new Enemy.Args(spawnPositions[randomPosition].position, parentObject));
        }
    }
    #endregion
    #endregion
}
