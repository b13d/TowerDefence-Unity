using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagment : MonoBehaviour
{
    [SerializeField]
    List<Spawner> _spawners = new List<Spawner>();

    int _spawnCountFinished = 0;
    int _finishLevel = 0;
    int _targetEnemyOnLevel = 0;

    public void AddTargetEnemy(int value)
    {
        _targetEnemyOnLevel += value;

        LevelLogic.instance.SetTextCountTargetEnemy = _targetEnemyOnLevel.ToString();
    }

    public void CompleteSpawnEnemy()
    {
        _spawnCountFinished++;

        if (_spawnCountFinished == _spawners.Count)
        {
            StartCoroutine(NextWave());
            _spawnCountFinished = 0;
        }
    }

    public void FinishLevelSpawnEnemy()
    {
        _finishLevel++;

        if (_finishLevel == _spawners.Count)
        {
            LevelLogic.instance.FinishedLevel();
        }
    }



    IEnumerator NextWave()
    {
        Debug.Log("Pause start");

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Pause off");

        for (int i = 0; i < _spawners.Count; i++)
        {
            _spawners[i].SetEnemySpawner();
        }

        LevelLogic.instance.ShowNewWave();
    }

}
