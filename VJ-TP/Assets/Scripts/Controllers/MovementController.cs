using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour, IMoveable {

		//user stats
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    public float JumpHeight => GetComponent<Actor>().ActorStats.JumpHeight;
		public float RayCastHeightOffset => GetComponent<Actor>().ActorStats.RayCastHeightOffset;

		//global stats
		[SerializeField] private GlobalStats globalStats;
		public float Gravity => globalStats.Gravity;

		[SerializeField] private Transform targetCamTransform;
		[SerializeField] private Transform gunTransform;

		[SerializeField] private LayerMask targetLayer;
		[SerializeField] private float playerHeightOffset = 1f;
		[SerializeField] private float raycastMaxDistance = 0.6f;

		[SerializeField] private float positionOffset = 5f;

		private Rigidbody rigidbody;
		private RaycastHit hit;
		private Vector3 raycastOrigin;
		private Vector3 targetPosition;

		private float airTimer;

		private bool _areControllersFrozen;

		void Start(){
			rigidbody = GetComponent<Rigidbody>();
			airTimer = 0f;
			EventsManager.instance.StartIntroCutscene += FreezeControllers;
			EventsManager.instance.FinishIntroCutscene += UnfreezeControllers;
		}

    public void Travel(Vector3 direction){
			if(!_areControllersFrozen){
				Vector3 newDir = targetCamTransform.TransformDirection(direction * Time.deltaTime * Speed);
				newDir.y = 0f;
				//shoot raycast in the direction we are looking at, if we found something, ignore movement
				Debug.DrawRay(transform.position + new Vector3(0,playerHeightOffset,0), newDir * 10f, Color.red);
				if(!Physics.Raycast(transform.position, newDir, out hit, raycastMaxDistance, targetLayer)){
					transform.Translate(newDir, Space.World);
				}
			}
		}

    public void Rotate(Vector3 direction){ //TODO review unused param
			if(!_areControllersFrozen){
				transform.rotation = targetCamTransform.rotation;
				gunTransform.rotation = targetCamTransform.rotation;
			}
		}

		public void Jump(){
			if(!_areControllersFrozen){
				rigidbody.AddForce(Vector3.up * Gravity * JumpHeight);
			}
		}

		public bool isFlying(){
			//calculate the origin from where the raycast will be thrown
			raycastOrigin = transform.position;
			raycastOrigin.y += RayCastHeightOffset;
			targetPosition = transform.position;
			//Vector3 raycastDirection = new Vector3(0, 1 * Mathf.Sin(transform.rotation.y), 0);
			//Draw Raycast
			Debug.DrawRay(raycastOrigin,  Vector3.down * 10f, Color.red);
			if(Physics.Raycast(raycastOrigin, Vector3.down, out hit, raycastMaxDistance)){
				targetPosition.y = hit.point.y + playerHeightOffset;
				transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
				rigidbody.velocity = Vector3.zero;
				airTimer = 0f;
				return false;
			}
			airTimer += Time.deltaTime;
			return true;
		}

		void FixedUpdate(){
			if (isFlying()) Fall();
		}

		private void Fall(){
			if(!_areControllersFrozen){
				rigidbody.AddForce(Vector3.down * Gravity * airTimer); //the longer you are in the air, the faster you fall
			}
		}

		void FreezeControllers(){
				_areControllersFrozen = true;
		}

		void UnfreezeControllers(){
				_areControllersFrozen = false;
		}

}
