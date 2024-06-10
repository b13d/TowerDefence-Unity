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
    float _beginValueCountDonw;
    
    public int spawnCountEnemy = 0;
    public int currentWave;
    public int[] waveNumber;

    public int counter = 0;


    public int GetCurrentCountEnemy
    {
        get { return waveNumber[currentWave]; }
    }

    private void Awake()
    {
        _countDown = _beginValueCountDonw;
    }

    private void Start()
    {
        Debug.Log($"spawnCountEnemy {spawnCountEnemy}");

        spawnCountEnemy = waveNumber[currentWave]; 
    }

    void Update()
    {
        _countDown -= Time.deltaTime;

        Debug.Log($"spawnCountEnemy: {spawnCountEnemy}");

        if (_countDown < 0 && spawnCountEnemy > 0)
        {
            _countDown = _beginValueCountDonw;

            counter++;

            spawnCountEnemy -= 1;

            //GameManager.instance.UpdateCountEnemy(counter);

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // создание противника, и присвоение ему пути его следования

        _beginValueCountDonw = Random.Range(0.1f, 1);

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

    public void NextWave()
    {
        if (currentWave + 1 < waveNumber.Length)
        {
            currentWave++;
            spawnCountEnemy = waveNumber[currentWave];
        } else
        {
            // завершение уровня
            LevelLogic.instance.FinishedLevel();
        }

    }
}
