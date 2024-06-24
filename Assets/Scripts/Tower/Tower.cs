using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    [Header("Tower Objects")]
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    List<Skill> _skills = new List<Skill>();
    [SerializeField]
    GameObject _gunSmoke;
    public TowerMenu towerMenu;
    [SerializeField]
    RadiusTower _radiusTower;
    [SerializeField]
    GameObject _shootTower;

    [Header("Tower Values")]
    public float damage = 1.0f;
    public float speedAttack = .6f;
    float changesSpeedAttack;
    const float SMOKEX = 1;
    const float SMOKEY = 1;


    LineRenderer _line;
    [SerializeField]
    float markup;
    [SerializeField]
    bool _useSmokeShoot;

    public float GetMarkup => markup;

    #region Methods

    private void Start()
    {
        changesSpeedAttack = speedAttack;
        towerMenu.UpdateTexts(damage, speedAttack);
        towerMenu.UpdateRadiusText(_radiusTower.xradius);
        _line = _radiusTower.gameObject.GetComponent<LineRenderer>();
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
        var shootSound = Instantiate(_shootTower, transform.position, Quaternion.identity);

        Destroy(shootSound, 2f);

        if (_useSmokeShoot)
        {
            Vector2 posSmoke = new Vector2(Random.Range(-SMOKEX, SMOKEX), Random.Range(-SMOKEY, SMOKEY));
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

    #endregion

}
