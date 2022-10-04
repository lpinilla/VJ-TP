using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour, IMoveable
{

		//user stats
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    public float JumpHeight => GetComponent<Actor>().ActorStats.JumpHeight;
		public float RayCastHeightOffset => GetComponent<Actor>().ActorStats.RayCastHeightOffset;

		//global stats
		[SerializeField] private GlobalStats globalStats;
		public float Gravity => globalStats.Gravity;

		public Camera main_camera;

		[SerializeField] private LayerMask targetLayer;

		private Rigidbody rigidbody;
		private RaycastHit hit;
		private Vector3 raycastOrigin;
		private Vector3 targetPosition;

		private float airTimer;

		void Start(){
			rigidbody = GetComponent<Rigidbody>();
			airTimer = 0f;
		}

    public void Travel(Vector3 direction){
			Vector3 newDir = main_camera.transform.TransformDirection(direction * Time.deltaTime * Speed);
			newDir.y = 0f;
			transform.Translate(newDir, Space.World);
		}
    public void Rotate(Vector3 direction)
        => transform.Rotate(direction * Time.deltaTime * RotationSpeed);

		public void Jump(){
			rigidbody.AddForce(Vector3.up * Gravity * JumpHeight);
		}

		public bool isFlying(){
			//calculate the origin from where the raycast will be thrown
			raycastOrigin = transform.position;
			raycastOrigin.y += RayCastHeightOffset;
			targetPosition = transform.position;
			//shoot raycast
			if(Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out hit, targetLayer)){
				targetPosition.y = hit.point.y + 1f;
				transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
				rigidbody.velocity = Vector3.zero;
				airTimer = 0f;
				return false;
			}
			airTimer += Time.deltaTime;
			return true;
		}

		public void Update(){
			if (isFlying()) Fall();
		}

		private void Fall(){
			rigidbody.AddForce(Vector3.down * Gravity * airTimer); //the longer you are in the air, the faster you fall
		}

}
