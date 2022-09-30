using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable
{
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    public float JumpHeight => GetComponent<Actor>().ActorStats.JumpHeight;
		public float FlyThreshold => GetComponent<Actor>().ActorStats.FlyThreshold;

		public Camera main_camera;


    public void Travel(Vector3 direction){
			Vector3 newDir = main_camera.transform.TransformDirection(direction * Time.deltaTime * Speed);
			newDir.y = 0f; //Force the y coord to remain zero and be dictated by player jump
			transform.Translate(newDir, Space.World);
		}
    public void Rotate(Vector3 direction)
        => transform.Rotate(direction * Time.deltaTime * RotationSpeed);
		public void Jump()
        => transform.Translate(Vector3.up * Time.deltaTime * JumpHeight);
		public bool isFlying()
        => transform.position.y > FlyThreshold; //to make the compiler shut up

		void Update(){
			if (isFlying()) Fall();
		}

		private void Fall(){
			transform.Translate(Vector3.down * Time.deltaTime * 3.0f, Space.World);
		}

}
