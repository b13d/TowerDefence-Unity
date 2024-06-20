using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public static ClickSound instance;

    [SerializeField]
    GameObject _clickSound;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var soundClick = Instantiate(_clickSound, transform);
            Destroy(soundClick, 1f);
        }
    }
}
