using UnityEngine;

[CreateAssetMenu(fileName = "ExplodableStats", menuName = "Stats/Explodable", order = 0)]
public class ExplodableStats : ScriptableObject
{
    [SerializeField] private ExplodableStatValues _ExplodableStatsValues;

    public GameObject ExplosionParticlePrefab => _ExplodableStatsValues.ExplosionParticlePrefab;
    public float Damage => _ExplodableStatsValues.Damage;
    public float PushbackFactor => _ExplodableStatsValues.PushbackFactor;
    public float Radius => _ExplodableStatsValues.Radius;
}

[System.Serializable]
public struct ExplodableStatValues
{
    public GameObject ExplosionParticlePrefab;
    public float Damage;
    public float PushbackFactor;
    public float Radius;
}
