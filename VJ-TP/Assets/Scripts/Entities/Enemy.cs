using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats EnemyStats => _enemyStats;
    [SerializeField] private EnemyStats _enemyStats;
}
