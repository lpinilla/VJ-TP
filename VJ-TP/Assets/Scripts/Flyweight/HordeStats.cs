using UnityEngine;

[CreateAssetMenu(fileName = "HordesStats", menuName = "Stats/Hordes", order = 0)]
public class HordeStats : ScriptableObject
{
    [SerializeField] private HordeStatValues _HordeStatsValues;

    public Enemy EnemyPrefab => _HordeStatsValues.EnemyPrefab;
    public int EnemiesInFirstRound => _HordeStatsValues.EnemiesInFirstRound;
    public int EnemiesInSecondRound => _HordeStatsValues.EnemiesInSecondRound;

}

[System.Serializable]
public struct HordeStatValues{
    public Enemy EnemyPrefab;
    public int EnemiesInFirstRound;
    public int EnemiesInSecondRound;
}
