using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField]
    private GameObject[] _prefabsEnemy;

    [SerializeField]
    private GameObject _pathEnemy;

    [SerializeField]
    private GameObject _castle;

    [SerializeField]
    private Transform _placeSpawn;

    [Header("Spawner Values")]
    [SerializeField]
    List<int> countEnemyWave = new List<int>();

    public int spawnCountEnemy = 0;

    public int counter = 0;

    #region Private Fields
    // private SpawnerManagment _spawnerManagment;

    float _countDown;

    [SerializeField]
    float _beginValueCountDown;

    int _targetCountEnemy;

    [SerializeField]
    bool _isLiveStage;

    bool _isPause;

    #endregion


    #region Methods

    private void Awake()
    {
        _countDown = _beginValueCountDown;

        // _spawnerManagment = transform.parent.GetComponent<SpawnerManagment>();
    }

    private void Start()
    {
        InitialValues();
    }

    void InitialValues()
    {
        spawnCountEnemy = countEnemyWave[0];

        _targetCountEnemy = spawnCountEnemy;

        int countEnemyOnLevel = 0;

        foreach (int enemyOnWave in countEnemyWave)
        {
            countEnemyOnLevel += enemyOnWave;
        }

        countEnemyWave.RemoveAt(0);

        if (!_isLiveStage)
        {
            // _spawnerManagment.AddTargetEnemy(countEnemyOnLevel);
        }
    }

    void Update()
    {
        if (!_isLiveStage && (_isPause || LevelLogic.instance.pause)) { return; }

        _countDown -= Time.deltaTime;

        if ((_countDown < 0 && spawnCountEnemy > 0)
            || (_isLiveStage && _countDown < 0))
        {
            _countDown = _beginValueCountDown;

            counter++;

            spawnCountEnemy -= 1;

            SpawnEnemy();
        }

        StatusSpawn();
    }

    void StatusSpawn()
    {
        if (spawnCountEnemy == 0 && !_isLiveStage && _placeSpawn.childCount == 0)
        {
            if (countEnemyWave.Count > 0)
            {
                _isPause = true;
                // _spawnerManagment.CompleteSpawnEnemy();
            }
            else
            {
                _isPause = true;
                // _spawnerManagment.FinishLevelSpawnEnemy();
            }
        }
    }

    public void SetEnemySpawner()
    {
        spawnCountEnemy = countEnemyWave[0];
        _targetCountEnemy += spawnCountEnemy;
        countEnemyWave.RemoveAt(0);
        _isPause = false;
    }

    void SpawnEnemy()
    {
        // the creation of an enemy, and assigning him his path of travel

        _beginValueCountDown = Random.Range(0.1f, 1);

        int rnd = Random.Range(0, _prefabsEnemy.Length);

        List<Vector2> paths = NewPathEnemy();

        var enemyPrefab = Instantiate(_prefabsEnemy[rnd], transform.position, Quaternion.identity, _placeSpawn);
        Enemy enemy = enemyPrefab.GetComponent<Enemy>();

        if (_isLiveStage)
        {
            enemy.isLiveStage = true;
        }

        enemy.levelEnemy = 0;
        enemy.lastTarget = _castle;
        enemy.path = paths;
    }

    List<Vector2> NewPathEnemy()
    {
        List<Vector2> paths = new List<Vector2>();

        for (int i = 0; i < _pathEnemy.transform.childCount; i++)
        {
            paths.Add(_pathEnemy.transform.GetChild(i).transform.position);
        }

        return paths;
    }

    #endregion



}
