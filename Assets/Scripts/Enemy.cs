using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Vector3> points;
    [SerializeField] private GameObject particleDead;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject particleDamage;

    private Collider _collider;
    private Animator _animator;

    public int currentPointIndex = 0;
    public int currentHealth;
    public int damage;
    public float speed;
    
    private bool isDied;
    public EnemyDataSO enemyData;

    public int MaxHealth => enemyData.health;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        currentHealth = enemyData.health;
        damage = enemyData.damage * Mathf.FloorToInt(LevelManager.Instance.IncreaseDamageEnemy);
        speed = Random.Range(enemyData.minSpeed, enemyData.maxSpeed) * Mathf.FloorToInt(LevelManager.Instance.IncreaseSpeedEnemy);
        
        Debug.Log("Урон врага: " + damage);
    }

    public void SetPoints(List<Vector3> points)
    {
        this.points = points;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        Instantiate(particleDamage, transform.position, particleDamage.transform.rotation); 

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (isDied) yield return null;
        
        isDied = true;
        int money = Random.Range(enemyData.coinsMin, enemyData.coinsMax) * Mathf.FloorToInt(LevelManager.Instance.IncreaseGoldEnemy);
        healthBar.SetActive(false);
        _animator.SetTrigger("isDead");
        GameManager.Instance.AddScore();
        GameManager.Instance.AddMoney(money);
        _collider.enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);
        // Instantiate(particleDead, transform.position, particleDead.transform.rotation);

        GameManager.Instance.DeadEnemy();
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0) return;

        Vector3 position = points[currentPointIndex];
        position.y = 0f;
        transform.position = Vector3.MoveTowards(transform.position, position, 0.05f * speed);

        if (Vector3.Distance(transform.position, position) < 0.1f)
        {
            if (currentPointIndex < points.Count - 1)
            {
                currentPointIndex++;
            }
        }
    }
}