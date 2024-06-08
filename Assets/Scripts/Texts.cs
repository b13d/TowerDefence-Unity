using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Texts : MonoBehaviour
{
    public TextMeshProUGUI _txtFinishedEnemy;
    public TextMeshProUGUI _txtCountWave;
    public TextMeshProUGUI _txtCountEnemy;

    [SerializeField] TextMeshProUGUI _txtHealth;
    [SerializeField] TextMeshProUGUI _txtMoney;

    void Start()
    {
        //_txtFinishedEnemy.text = $"Enemy Finished {_finishedEnemy}";
        //_txtCountWave.text = $"{spawnerEnemy.currentWave + 1}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
