using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats EnemyStats => _actorStats;
    [SerializeField] private EnemyStats _actorStats;
}
