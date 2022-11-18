using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour, IEnemy
{

	private UnityEngine.AI.NavMeshAgent navMeshAgent;


  public float DetectionRange => GetComponent<Enemy>().EnemyStats.DetectionRange;
  public float AttackRange => GetComponent<Enemy>().EnemyStats.AttackRange;
  public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;

	void Awake(){
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	public bool isWithinDetectionRange(Vector3 targetPosition){
		return Vector3.Distance(targetPosition, this.transform.position) <= DetectionRange;
	}

	public bool isWithinAttackRange(Vector3 targetPosition){
		//Debug.Log(Vector3.Distance(targetPosition, this.transform.position));
		return Vector3.Distance(targetPosition, this.transform.position) <= AttackRange;
	}


  public void Follow(Vector3 targetPosition){
			navMeshAgent.destination = targetPosition;
			navMeshAgent.speed = Speed;
	}

	public void StopFollowing(){
			navMeshAgent.destination = transform.position; //set destination to my own position to stop path finding
			navMeshAgent.speed = 0f;
	}

  public void Attack(){

	}




}
