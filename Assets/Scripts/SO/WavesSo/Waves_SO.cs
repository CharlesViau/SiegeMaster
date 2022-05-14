using General;
using Managers;
using UnityEngine;
using Units.Types;
using System.Collections.Generic;

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
    [SerializeField] List<CountPerEnemy> enemiesList;
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
        foreach (CountPerEnemy enemy in enemiesList)
            SpawnEnemies(enemy.enemyType, enemy.count);
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

    #region Manage Spawning Tool
    [System.Serializable]
    public class CountPerEnemy
    {
        public EnemyType enemyType;
        public int count;
    }
    #endregion

    #region old version
    /*[SerializeField] int nbOfArcherEnemies;
    [SerializeField] int nbOfSneakyEnemies;
    [SerializeField] int nbOfWarriorEnemies;
    [SerializeField] int nbOfBossEnemies;
    public int NbOfArcherEnemies { get { return nbOfArcherEnemies; } }
    public int NbOfSneakyEnemies { get { return nbOfSneakyEnemies; } }
    public int NbOfWarriorEnemies { get { return nbOfWarriorEnemies; } }
    public int NbOfBossEnemies { get { return nbOfBossEnemies; } }*/
    #endregion
}
