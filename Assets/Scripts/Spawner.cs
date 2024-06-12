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

    float _countDown;

    [SerializeField]
    float _beginValueCountDown;
    
    public int spawnCountEnemy = 0;

    public List<int> countEnemyWave = new List<int>();

    public int targetSpawnCountEnemy;

    bool _isPause;

    public int counter = 0;


    private void Awake()
    {
        _countDown = _beginValueCountDown;
    }

    private void Start()
    {
        targetSpawnCountEnemy = countEnemyWave[0];

        countEnemyWave.RemoveAt(0);
    }

    void Update()
    {
        if (_isPause) { return; }

        _countDown -= Time.deltaTime;

        if (_countDown < 0 && spawnCountEnemy > 0)
        {
            _countDown = _beginValueCountDown;

            counter++;

            spawnCountEnemy -= 1;

            SpawnEnemy();
        }

        if (spawnCountEnemy == 0)
        {
            if (countEnemyWave.Count > 0) 
            {
                spawnCountEnemy = countEnemyWave[0];

                countEnemyWave.RemoveAt(0);

                StartCoroutine(Pause());
            }
            else
            {
                Debug.LogError("Уровень завершен!!");
            }
        }
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

        var enemy = Instantiate(_prefabsEnemy[rnd], transform.position, Quaternion.identity);

        enemy.GetComponent<Enemy>().lastTarget = _castle;
        enemy.GetComponent<Enemy>().path = paths;
    }

    IEnumerator Pause()
    {
        _isPause = true;

        yield return new WaitForSeconds(1.5f);

        _isPause = false;
    }
}
