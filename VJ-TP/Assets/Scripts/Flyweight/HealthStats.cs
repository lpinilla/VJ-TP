using UnityEngine;

[CreateAssetMenu(fileName = "HealthStats", menuName = "Stats/Health", order = 0)]
public class HealthStats : ScriptableObject
{
    [SerializeField] private HealthStatValues _statValues;

		public float HealAmmount => _statValues.HealAmmount;
}

[System.Serializable]
public struct HealthStatValues
{
		public float HealAmmount;
}
