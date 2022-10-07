using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "Stats/Actor", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private ActorStatValues _statValues;

    public int MaxLife => _statValues.MaxLife;
    public float MovementSpeed => _statValues.MovementSpeed;
    public float RotationSpeed => _statValues.RotationSpeed; //note used
    public float JumpHeight => _statValues.JumpHeight;
		public float RayCastHeightOffset => _statValues.RayCastHeightOffset;
		public float CameraSensitivity => _statValues.CameraSensitivity;
}

[System.Serializable]
public struct ActorStatValues
{
    public int MaxLife;
    public float MovementSpeed;
    public float RotationSpeed; //not used
		public float JumpHeight;
		public float RayCastHeightOffset;
		public float CameraSensitivity;
}
