using UnityEngine;

[CreateAssetMenu(fileName = "GlobalStats", menuName = "Stats/Global", order = 0)]
public class GlobalStats : ScriptableObject
{
    [SerializeField] private GlobalStatValues _statValues;

		public float Gravity => _statValues.gravity;
}

[System.Serializable]
public struct GlobalStatValues
{
		public float gravity;
}
