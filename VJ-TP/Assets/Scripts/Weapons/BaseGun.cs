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

		public GameObject bulletPrefab => _stats.BulletPrefab;
		public GameObject muzzleFlashPrefab => _stats.MuzzleFlashPrefab;

		private Transform bulletInstanceTransform;

		private Renderer gunRenderer;
		
		public AudioSource audioSource;

    private void Start() {
			bulletInstanceTransform = transform.Find("BulletFireTransform");
			gunRenderer = GetComponent<Renderer>();
      Reload();
    }

    public virtual void Attack() {
        if (_bulletCount > 0) {
					//create muzzle flash
					Instantiate(muzzleFlashPrefab, bulletInstanceTransform.position, bulletInstanceTransform.rotation);
					//create bullet
            var bullet = Instantiate(bulletPrefab, bulletInstanceTransform.position, bulletInstanceTransform.rotation);
						bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
            _bulletCount--;
            UI_AmmoUpdater();
            audioSource.Play();
        }
    }

    public void Reload() {
        _bulletCount = MagSize;
        UI_AmmoUpdater();
    }

		public void UI_AmmoUpdater() {
				//change Gun Color Intensity
				//get percentage
				float bulletCountPercentage = (float)_bulletCount / MagSize;
				//change emission color based on bullet count
				gunRenderer.material.SetColor("_EmissionColor", new Color(bulletCountPercentage, 0, 0, 0));
		}

}
