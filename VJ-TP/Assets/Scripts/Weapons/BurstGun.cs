using UnityEngine;

public class BurstGun : BaseGun
{
    [SerializeField] private int _shotCount = 3;
    [SerializeField] private float _bullet_displacement = 0.6f;

    public override void Attack()
    {
        if (_bulletCount > 0) {
            for (int i = 0; i < _shotCount; i++) {
                var bullet = Instantiate(
                    BulletPrefab,
                    transform.position + Vector3.forward * i * _bullet_displacement,
                    Quaternion.identity);
                bullet.name = "Burst Gun Bullet";
                bullet.GetComponent<Bullet>().SetOwner(this);
                _bulletCount--;
            }
            //UI_AmmoUpdater();
        }
    }
}
