using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    private Enemy enemy;
    private Tower tower;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        tower = GetComponentInParent<Tower>();
    }

    private void Update()
    {
        if (enemy != null)
        {
            fillImage.fillAmount = (float)enemy.currentHealth / enemy.MaxHealth;
        }

        if (tower != null)
        {
            Debug.Log("Health tower: " + tower.health);
            fillImage.fillAmount = (float)tower.health / tower.MaxHealth;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180f, 0);
    }
}
