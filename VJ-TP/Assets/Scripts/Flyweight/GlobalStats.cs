using UnityEngine;

[CreateAssetMenu(fileName = "GlobalStats", menuName = "Stats/Global", order = 0)]
public class GlobalStats : ScriptableObject
{
    [SerializeField] private GlobalStatValues _statValues;

		public float Gravity => _statValues.Gravity;
}

[System.Serializable]
public struct GlobalStatValues
{
		public float Gravity;
}
