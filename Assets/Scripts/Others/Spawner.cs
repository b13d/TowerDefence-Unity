using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabsEnemy;

    [SerializeField]
    private GameObject _pathEnemy;

    [SerializeField]
    private GameObject _castle;

    [SerializeField]
    private Transform _placeSpawn;

    private SpawnerManagment _spawnerManagment;

    float _countDown;

    [SerializeField]
    float _beginValueCountDown;

    int _targetCountEnemy;

    public int spawnCountEnemy = 0;

    [SerializeField]
    List<int> countEnemyWave = new List<int>();

    [SerializeField]
    bool _isLiveStage;

    bool _isPause;

    public int counter = 0;


    private void Awake()
    {
        _countDown = _beginValueCountDown;

        _spawnerManagment = transform.parent.GetComponent<SpawnerManagment>();
    }

    private void Start()
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
            _spawnerManagment.AddTargetEnemy(countEnemyOnLevel);
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

        if (spawnCountEnemy == 0 && !_isLiveStage && _placeSpawn.childCount == 0)
        {
            if (countEnemyWave.Count > 0)
            {
                _isPause = true;
                _spawnerManagment.CompleteSpawnEnemy();
            }
            else
            {
                _isPause = true;
                _spawnerManagment.FinishLevelSpawnEnemy();
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
        // создание противника, и присвоение ему пути его следования

        _beginValueCountDown = Random.Range(0.1f, 1);

        int rnd = Random.Range(0, _prefabsEnemy.Length);

        List<Vector2> paths = new List<Vector2>();

        for (int i = 0; i < _pathEnemy.transform.childCount; i++)
        {
            paths.Add(_pathEnemy.transform.GetChild(i).transform.position);
        }

        var enemy = Instantiate(_prefabsEnemy[rnd], transform.position, Quaternion.identity, _placeSpawn);

        if (_isLiveStage)
        {
            enemy.GetComponent<Enemy>().isLiveStage = true;
        }

        enemy.GetComponent<Enemy>().levelEnemy = 0;
        enemy.GetComponent<Enemy>().lastTarget = _castle;
        enemy.GetComponent<Enemy>().path = paths;
    }


}
