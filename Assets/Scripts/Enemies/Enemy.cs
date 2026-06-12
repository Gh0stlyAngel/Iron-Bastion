using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    private float _health;
    public float MaxHealth;
    public int DamageToBase;
    public int GoldReward;
    public int Armor;

    [SerializeField] private float _deathDuration = 0.3f;

    public event Action<Enemy> Died;

    private const float ArmorReductionPerPoint = 0.025f;

    public bool IsDead => _isDead;
    [SerializeField] private bool _isDead;

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
            StartCoroutine(DamageFlash());
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
        //Destroy(gameObject);
        StartCoroutine(DeathAnimation());
    }

    public void OnReachedBase(Enemy _)
    {
        Destroy(gameObject);
    }

    private IEnumerator DamageFlash()
    {
        _renderer.material = _takeDamageMaterial;
        yield return new WaitForSeconds(0.125f);
        _renderer.material = _defaultMaterial;
    }

    private IEnumerator DeathAnimation()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;

        Vector3 startScale = transform.localScale;
        float progress = 0f;
        while (progress < 1f)
        {
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);

            progress += Time.deltaTime / _deathDuration;

            yield return null;
        }

        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}
