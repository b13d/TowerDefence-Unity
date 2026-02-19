using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemy;
    public int countSpawn = 0;
    public List<GameObject> enemyRoad = new List<GameObject>();

    // IEnumerator Spawn()
    // {
    //     while (true)
    //     {
    //         int rand = Random.Range(1, 6);
    //         
    //         Debug.Log("rand: " + rand);
    //
    //         for (int i = 0; i < rand; i++)
    //         {
    //             yield return new WaitForSeconds(0.1f * i);
    //             Debug.Log("Появление нового врага!");
    //             Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
    //             var newEnemy = Instantiate(prefabEnemy, position, Quaternion.identity);
    //             Enemy enemy = newEnemy.GetComponent<Enemy>();
    //             // enemy.SetPoints(enemyRoad);
    //             countSpawn++;
    //         }
    //
    //         yield return new WaitForSeconds(1.5f);
    //     }
    //     
    //     Debug.Log("Враги закончились!");
    // }
}