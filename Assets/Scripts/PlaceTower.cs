using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlaceTower : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] List<Transform> pointsWay = new List<Transform>();
    [SerializeField] private GameObject ground;
    [SerializeField] private bool isActiveTower;
    [SerializeField] private GameObject disabledTower;
    [SerializeField] private TextMeshProUGUI textCostTower;
    Coroutine _recoveryMoneyCoroutine;
    
    private int _costTower = 0;
    private int _defaultCostTower = 0;
    
    public int currentPoint;

    private void Start()
    {
        _costTower = towerPrefab.GetComponent<Tower>().TowerData.money;
        _defaultCostTower = _costTower;
        textCostTower.text = _costTower + "$";
    }

    void Update()
    {
        if (isActiveTower || Time.timeScale == 0) return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("PlaceTower") && hit.collider.gameObject == gameObject)
                {
                    if (GameManager.Instance.money >= _costTower)
                    {
                        Debug.Log("Покупка башни!" + gameObject);
                        GameManager.Instance.BuyTower(_costTower);
                    }
                    else
                    {
                        LevelManager.Instance.Notification("Недостаточно средств для покупки башни.", TypeMessage.Info);
                        return;
                    }
                    
                    Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z);
                    var newTower = Instantiate(towerPrefab, newPosition, transform.rotation);
                    newTower.GetComponent<Tower>().PlaceTower = this; 
                    DisablePlace();
                    Debug.Log(hit.collider.name);
                }
            }
        }
    }

    void DisablePlace()
    {
        isActiveTower = true;
        ground.SetActive(false);
        disabledTower.SetActive(false);

        if (_recoveryMoneyCoroutine != null)
        {
            StopCoroutine(_recoveryMoneyCoroutine);
        }
    }

    public void ActivePlace()
    {
        // Повышаем временно стоимость башни | B - balance
        _costTower = _defaultCostTower * 2;
        textCostTower.text = _costTower + "$";
        
        isActiveTower = false;
        ground.SetActive(true);
        disabledTower.SetActive(true);
        _recoveryMoneyCoroutine = StartCoroutine(RecoveryMoney());
    }

    IEnumerator RecoveryMoney()
    {
        while (_costTower != _defaultCostTower)
        {
            yield return new WaitForSeconds(0.15f);

            _costTower -= 1;
            textCostTower.text = _costTower + "$";
        }
    }
    
    public void FixedUpdate()
    {
        if (isActiveTower) return;

        bool distance = 0.1f > Vector3.Distance(pointsWay[currentPoint].position, transform.position);

        if (distance)
        {
            currentPoint++;
            currentPoint %= pointsWay.Count;
        }

        transform.position =
            Vector3.MoveTowards(transform.position, pointsWay[currentPoint].position, Time.deltaTime * 2f);
    }
}