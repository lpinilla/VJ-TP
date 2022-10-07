using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour, IEnemy
{

	private UnityEngine.AI.NavMeshAgent navMeshAgent;


  public float DetectionRange => GetComponent<Enemy>().EnemyStats.DetectionRange;

	void Awake(){
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	public bool isWithinRange(Vector3 targetPosition){
		return Vector3.Distance(targetPosition, this.transform.position) <= DetectionRange;
	}

  public void Follow(Vector3 targetPosition){
			navMeshAgent.destination = targetPosition;
	}

	public void StopFollowing(){
			navMeshAgent.destination = transform.position; //set destination to my own position to stop path finding
	}

  public void Attack(){

	}




}
