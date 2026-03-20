using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    
    [SerializeField]
    List<Image> healths = new List<Image>();

    [Range(0, 10)]
    public int health;
    public int oldHealth;

    private void Start()
    {
        GameManager.Instance.OnHealthChanged += Health;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnHealthChanged -= Health;
    }

    void Health(int _health)
    {
        health = _health;
        
        if (oldHealth != health)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
            
            healths.Clear();   
            oldHealth = health;

            for (int i = 0; i < health; i++)
            {
                healths.Add(healthImage);
                Instantiate(healthImage, transform);
            }
        }
    }
    
    private void OnValidate()
    {
        Debug.Log("health: " + health);

        if (oldHealth != health)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
            
            healths.Clear();   
            oldHealth = health;

            for (int i = 0; i < health; i++)
            {
                healths.Add(healthImage);
                Instantiate(healthImage, transform);
            }
        }
    }
}
