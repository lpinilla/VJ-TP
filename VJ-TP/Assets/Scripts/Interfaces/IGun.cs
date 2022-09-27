using UnityEngine;

public interface IGun
{
    gameobject bulletprefab { get; }

    int magsize { get; } //how many bullets can the gun hold
    int bulletcount { get; } //how many bullets are left

    float cooldown { get; } //how many milliseconds between each shot

    void attack(); //shoot
    void reload(); //reload
}
