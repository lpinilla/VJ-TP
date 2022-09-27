using UnityEngine;

public interface IBullet
{
    Gun Owner { get; } //who shot it


    float LifeTime { get; } //how long will it live
    float Speed { get; } // the bullet's speed
		float drop { get; } //drop rate


    Rigidbody Rigidbody { get; } //bullet's rigidbody
    Collider Collider { get; } //bullet's collider

    void Travel();
    void OnTriggerEnter(Collider collider);
    void SetOwner(Gun gun);
}
