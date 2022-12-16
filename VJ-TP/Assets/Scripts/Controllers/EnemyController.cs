using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour, IEnemy
{

	private UnityEngine.AI.NavMeshAgent navMeshAgent;


  public float DetectionRange => GetComponent<Enemy>().EnemyStats.DetectionRange;
  public float AttackRange => GetComponent<Enemy>().EnemyStats.AttackRange;
  public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;

  public bool isDead => _isDead;
  private bool _isDead;

  private Rigidbody[] rigidbodies;
  private Collider[] colliders;

	void Awake(){
		_isDead = false;
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Start(){
		setRigidbodyState(true);
        setColliderState(false);
        GetComponent<Animator>().enabled = true;
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

	public void StartDying(){
		Destroy(gameObject, 3f);
		GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
		_isDead = true;
		EventsManager.instance.EnemyDeath();
	}

	void setRigidbodyState(bool state){
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies){
            rigidbody.isKinematic = state;
        }
        gameObject.GetComponent<Rigidbody>().isKinematic = !state;

    }


    void setColliderState(bool state){
        colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders){
	        if(collider.tag != "EnemyDamage") collider.enabled = state;
            // collider.enabled = state;
        }
        gameObject.GetComponent<Collider>().enabled = !state;

    }

}
