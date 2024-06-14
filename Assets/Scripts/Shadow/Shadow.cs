using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    void Start()
    {
        int rnd = Random.Range(5, 35);
        Invoke("DestroySelf", rnd);
    }

    void DestroySelf()
    {
        GetComponent<Animator>().SetTrigger("HideShadow");
        Destroy(gameObject, 2f);
    }
}
