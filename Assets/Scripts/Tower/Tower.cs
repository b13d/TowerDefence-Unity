using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    List<Skill> _skills = new List<Skill>();

    [SerializeField]
    GameObject _gunSmoke;

    [SerializeField]
    float markup;

    [SerializeField]
    bool _useSmokeShoot;

    public TowerMenu towerMenu;

    [SerializeField]
    RadiusTower _radiusTower;
    
    
    public float damage = 1.0f;
    public float speedAttack = .6f;
    float changesSpeedAttack;

    public float GetMarkup
    {
        get { return markup; }
    }

    private void Start()
    {
        changesSpeedAttack = speedAttack;
        towerMenu.UpdateTexts(damage, speedAttack);
        towerMenu.UpdateRadiusText(_radiusTower.xradius);
    }

    void Update()
    {
        if (_target != null)
        {
            changesSpeedAttack -= Time.deltaTime;

            if (changesSpeedAttack < 0)
            {
                changesSpeedAttack = speedAttack;
                Shoot();
            }
        }
    }

    public void ChangeSpeedAttack(float value)
    {
        if (speedAttack - value > 0)
        {
            speedAttack -= value;
            changesSpeedAttack = speedAttack;

            towerMenu.UpdateTexts(damage, speedAttack);
        }
    }

    public void ChangeDamageTower(float value)
    {
        damage += value;

        towerMenu.UpdateTexts(damage, speedAttack);
    }

    void Shoot()
    {

        var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);

        if (_useSmokeShoot)
        {
            Vector2 posSmoke = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            var smoke = Instantiate(_gunSmoke, transform.position, Quaternion.identity, transform);
            smoke.transform.localPosition = posSmoke;

            Destroy(smoke, 1f);
        }

        Projectile projectilePrefab = projectile.GetComponent<Projectile>();

        projectilePrefab.targetEnemy = _target;
        projectilePrefab.Damage = damage;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.LogError($"У меня противник!! {collision.name}");

        if (collision.tag == "Enemy")
        {
            _target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _target)
        {
            _target = null;
        }
    }

}
