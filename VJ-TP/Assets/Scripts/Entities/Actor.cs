using UnityEngine;

public class Actor : MonoBehaviour
{
    public ActorStats ActorStats => _actorStats;
    [SerializeField] private ActorStats _actorStats;
}
