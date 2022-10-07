using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/Enemy", order = 0)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyStatValues _statValues;

		public float DetectionRange => _statValues.DetectionRange;
}

[System.Serializable]
public struct EnemyStatValues
{
		public float DetectionRange;
}
