using UnityEngine;

public interface IExplodable
{
    //GameObject ParticlePrefab { get; }

    float Radius { get; } //how many bullets can the gun hold
    float Damage { get; } //how many bullets are left

    void Explode(); //explode
}
