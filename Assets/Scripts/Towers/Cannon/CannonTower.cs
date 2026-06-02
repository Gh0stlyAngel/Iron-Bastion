using UnityEngine;

public class CannonTower : Tower
{

    [SerializeField] private CannonShell _shell;
    [SerializeField] private float _shellSpeed;
    [SerializeField] private float _shellDamage;
    [SerializeField] private float _shellExplosionRadius;
    protected override void Attack(Transform target)
    {
        CannonShell shell = Instantiate(_shell, _shootingPoint.transform.position, transform.rotation);

        shell.SetExplosionRadius(_shellExplosionRadius);

        shell.Shot(_shellDamage, _shellSpeed, target);
    }
}
