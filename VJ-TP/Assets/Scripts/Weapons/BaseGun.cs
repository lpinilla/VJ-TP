using UnityEngine;

public class BaseGun : MonoBehaviour, IBaseGun
{
    [SerializeField] private GunStats _stats;

    public GameObject BulletPrefab => _stats.BulletPrefab;
    public int MagSize => _stats.MagSize;
    public int Damage => _stats.Damage;
		public float BulletDrop => _stats.BulletDrop;

    public int BulletCount => _bulletCount;
    [SerializeField] protected int _bulletCount;

	public float Cooldown => _stats.Cooldown;

    private void Start() {
        Reload();
    }

    public virtual void Attack() {
        if (_bulletCount > 0) {
					//put a bullet into scene
            var bullet = Instantiate(
                BulletPrefab,
                transform.position,
                transform.rotation);
            bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
            _bulletCount--;
            //UI_AmmoUpdater();
        }
    }

    public void Reload() {
        _bulletCount = MagSize;
        //UI_AmmoUpdater();
    }

//    public void UI_AmmoUpdater() {
//        EventsManager.instance.AmmoChange(_bulletCount, _stats.MagSize);
//    }
}
