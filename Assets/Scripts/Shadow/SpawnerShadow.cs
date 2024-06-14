using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShadow : MonoBehaviour
{
    [SerializeField]
    GameObject _shadow;
    
    void Start()
    {
        InvokeRepeating("SpawnShadow", 1f, 5f);
    }

    void SpawnShadow()
    {
        float rnd = Random.Range(0.0f, 1.0f); 

        if (rnd >= .5f)
        {
            Vector2 newPos = new Vector2(Random.Range(-14, -11), Random.Range(-12, 9));

            var newShadow = Instantiate(_shadow, newPos, Quaternion.identity);

            newShadow.GetComponent<Rigidbody2D>().velocity = Vector2.right;
        } 
        else
        {
            Vector2 newPos = new Vector2(Random.Range(12, 15), Random.Range(-12, 9));

            var newShadow = Instantiate(_shadow, newPos, Quaternion.identity);

            newShadow.GetComponent<Rigidbody2D>().velocity = Vector2.left;
        }
    }

}
