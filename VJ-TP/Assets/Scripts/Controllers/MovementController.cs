using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable
{
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    public float JumpHeight => GetComponent<Actor>().ActorStats.JumpHeight;

    public void Travel(Vector3 direction)
        => transform.Translate(direction * Time.deltaTime * Speed);
    public void Rotate(Vector3 direction)
        => transform.Rotate(direction * Time.deltaTime * RotationSpeed);
		public void Jump()
        => transform.Translate(Vector3.up * Time.deltaTime * JumpHeight);
		public bool isFlying()
        => transform.position.y > 10.0f; //to make the compiler shut up

}
