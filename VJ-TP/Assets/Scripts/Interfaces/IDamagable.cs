using UnityEngine;

public interface IDamageable
{
    float MaxLife { get; } //Actor's life

    void TakeDamage(float damage); //receive damage

    void Heal(float healAmmount); //receive health

    void Die(); //terminate instance
}
