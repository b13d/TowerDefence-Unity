using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    const float RELOAD_TIME = 0.5f;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject prefabBullet;
    private Quaternion  defaultRotation;
    private Coroutine backCoroutine;
    
    public float reloadTime;
    public bool wasShot;
    public TowerData towerData;
    public PlaceTower placeTower;

    public int health;
    public int damage;
    public int level;
    public string towerName;

    public TowerData TowerData => towerData;
    public int MaxHealth => towerData.health;

    public PlaceTower PlaceTower
    {
        set { placeTower = value; }
        get { return placeTower; }
    }


    private void Awake()
    {
        defaultRotation = cannon.transform.rotation;
        health = towerData.health;
        damage = towerData.damage;
        level = towerData.level;
        towerName = towerData.nameTower;
    }

    private void Update()
    {
        // обратный отсчет, перезарядка после выстрела
        if (wasShot)
        {
            reloadTime -= Time.deltaTime;

            if (reloadTime <= 0)
            {
                reloadTime = RELOAD_TIME;
                wasShot = false;
            }
        }
    }

    IEnumerator BackToDefault()
    {
        yield return new WaitForSeconds(1f);

        float t = 0f;
        float duration = 0.3f; // скорость возврата

        Quaternion startRotation = cannon.transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            cannon.transform.rotation =
                Quaternion.Slerp(startRotation, defaultRotation, t);
            yield return null;
        }

        cannon.transform.rotation = defaultRotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !wasShot)
        {
            if (backCoroutine != null)
            {
                StopCoroutine(backCoroutine);
            }

            health -= 1;
            // Debug.Log("Выстрел с башни");
            wasShot = true;
            cannon.transform.LookAt(other.transform.position);
            backCoroutine = StartCoroutine(BackToDefault());
            var bullet = Instantiate(prefabBullet, cannon.transform.position, cannon.transform.rotation);
            Kernel kernel = bullet.GetComponent<Kernel>();
            kernel.Damage = damage;
            kernel.target = other.gameObject;
            Vector3 direction = (other.transform.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody>().AddForce(direction * 5, ForceMode.Impulse);

            if (health <= 0)
            {
                placeTower.ActivePlace();
                Destroy(gameObject);
            }
        }
    }
}