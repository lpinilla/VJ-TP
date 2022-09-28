using UnityEngine;

public interface IBullet
{
    BaseGun Owner { get; } //who shot it, defines the damage it does

    float LifeTime { get; } //how long will it live
    float Speed { get; } // the bullet's speed. It's here because it will be the same for all guns

    Rigidbody Rigidbody { get; } //bullet's rigidbody
    Collider Collider { get; } //bullet's collider

    void Travel();
    void OnTriggerEnter(Collider collider);
    void SetOwner(BaseGun gun);
}
