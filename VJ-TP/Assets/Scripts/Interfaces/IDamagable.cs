using UnityEngine;

public interface IDamageable
{
    float MaxLife { get; } //Actor's life

    void TakeDamage(float damage); //receive damage

    void Die(); //terminate instance
}
