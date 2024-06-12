using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private GameObject _projectile;

    [SerializeField]
    private TextMeshProUGUI _txtDamage;

    [SerializeField]
    private TextMeshProUGUI _txtSpeedAttack;

    [SerializeField]
    private GameObject _textsView;

    [SerializeField]
    private GameObject _skillsView;

    [SerializeField]
    List<Skill> _skills = new List<Skill>();

    [SerializeField]
    float markup;
    
    
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
        _textsView.SetActive(false);
        _skillsView.SetActive(false);

        _txtDamage.text = $"Урон: {damage}";
        _txtSpeedAttack.text = $"Скорость атаки: {speedAttack}";
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

            UpdateTexts();
        }
    }

    public void ChangeDamageTower(float value)
    {
        damage += value;

        UpdateTexts();
    }

    void UpdateTexts()
    {
        _txtDamage.text = $"Урон: {damage}";
        _txtSpeedAttack.text = $"Скорость атаки: {speedAttack}";
    }

    void Shoot()
    {
        var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);

        Projectile projectilePrefab = projectile.GetComponent<Projectile>();

        projectilePrefab.targetEnemy = _target;
        projectilePrefab.Damage = damage;
    }

    private void OnMouseEnter()
    {
        _textsView.SetActive(true);
    }


    private void OnMouseExit()
    {
        _textsView.SetActive(false);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        _skillsView.SetActive(!_skillsView.activeSelf);
    }


    public void DestoySelf()
    {
        Destroy(gameObject);
    }
}
