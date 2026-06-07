using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    private float _health;
    public float MaxHealth;
    public int DamageToBase;
    public int GoldReward;
    public int Armor;

    public event Action<Enemy> Died;

    private const float ArmorReductionPerPoint = 0.025f;

    private bool _isDead;

    private Renderer _renderer;
    private Material _defaultMaterial;
    [SerializeField] private Material _takeDamageMaterial;


    private void Awake()
    {
        _isDead = false;
        _health = MaxHealth;

        
    }

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _defaultMaterial = _renderer.material;
    }

    public void TakeDamage(float damage, DamageType damageType)
    {
        if (_isDead)
        {
            return;
        }
        switch (damageType)
        {
            case DamageType.Physical:
                float damageReduction = Armor * ArmorReductionPerPoint;
                _health -= damage * (1f - damageReduction);
                break;

            case DamageType.Magic:
                _health -= damage;
                break;

            default:
                Debug.LogWarning($"Unknown damage type: {damageType}");
                break;
        }

        if (_health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageFlash());
        }
    }

    public void Die()
    {
        _isDead = true;
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    public void OnReachedBase(Enemy _)
    {
        Destroy(gameObject);
    }

    private IEnumerator DamageFlash()
    {
        Debug.LogWarning("Took dmg!");
        _renderer.material = _takeDamageMaterial;
        yield return new WaitForSeconds(0.125f);
        _renderer.material = _defaultMaterial;
    }
}
