using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private ActorStatValues _statValues;

    public int MaxLife => _statValues.MaxLife;
    public float MovementSpeed => _statValues.MovementSpeed;
    public float RotationSpeed => _statValues.RotationSpeed;
    public float JumpHeight => _statValues.JumpHeight;
		public float RayCastHeightOffset => _statValues.RayCastHeightOffset;
}

[System.Serializable]
public struct ActorStatValues
{
    public int MaxLife;
    public float MovementSpeed;
    public float RotationSpeed;
		public float JumpHeight;
		public float RayCastHeightOffset;
}
