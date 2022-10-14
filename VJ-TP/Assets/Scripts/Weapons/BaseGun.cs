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
		[SerializeField] private GameObject bulletPrefab;

		private Transform bulletInstanceTransform;

		private Renderer gunRenderer;

    private void Start() {
			bulletInstanceTransform = transform.Find("BulletFireTransform");
			gunRenderer = GetComponent<Renderer>();
      Reload();
    }

    public virtual void Attack() {
        if (_bulletCount > 0) {
            var bullet = Instantiate(
							bulletPrefab, bulletInstanceTransform.position, bulletInstanceTransform.rotation
						);
						bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
            _bulletCount--;
            UI_AmmoUpdater();
        }
    }

    public void Reload() {
        _bulletCount = MagSize;
        UI_AmmoUpdater();
    }

		public void UI_AmmoUpdater() {
				//change Gun Color Intensity
				//discretize bullet count with 0-255
				float per = (float)_bulletCount / MagSize;
				//change emission color based on bullet count
				gunRenderer.material.SetColor("_EmissionColor", new Color(per, 0, 0, 0));
		}

}
