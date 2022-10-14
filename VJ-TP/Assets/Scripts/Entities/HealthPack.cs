using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthPack : MonoBehaviour, ICurable
{

    [SerializeField] private HealthStats _healthStats;

    public float HealAmmount => _healthStats.HealAmmount;

}
