using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Guns", order = 0)]
public class GunStats : ScriptableObject
{
    [SerializeField] private GunStatValues _gunStatsValues;

    public GameObject BulletPrefab => _gunStatsValues.BulletPrefab;
    public GameObject MuzzleFlashPrefab => _gunStatsValues.MuzzleFlashPrefab;
    public int Damage => _gunStatsValues.Damage;
    public int MagSize => _gunStatsValues.MagSize;
    public float Cooldown => _gunStatsValues.Cooldown;
    public float BulletDrop => _gunStatsValues.BulletDrop;
	public float BulletSpeed => _gunStatsValues.BulletSpeed;
    public float RateOfFire => _gunStatsValues.RateOfFire;
}

[System.Serializable]
public struct GunStatValues
{
    public GameObject BulletPrefab;
    public GameObject MuzzleFlashPrefab;
    public int Damage;
    public int MagSize;
    public float Cooldown;
    public float BulletDrop;
	public float BulletSpeed;
    public float RateOfFire;
}
