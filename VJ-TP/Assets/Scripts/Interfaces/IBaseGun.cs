using UnityEngine;

public interface IBaseGun
{
    GameObject BulletPrefab { get; }

    int MagSize { get; } //how many bullets can the gun hold
    int BulletCount { get; } //how many bullets are left

    float Cooldown { get; } //how many milliseconds between each shot
		float BulletDrop { get; } //the drop that bullets being shot by this gun will have

    void Attack(); //shoot
    void Reload(); //reload
}
