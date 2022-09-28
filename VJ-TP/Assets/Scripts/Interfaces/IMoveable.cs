using UnityEngine;

public interface IMoveable
{
    float Speed { get; } //actors's speed
    float RotationSpeed { get; } //actor's rotational speed
    float JumpHeight { get; } //actor's max height when jumping once

    void Travel(Vector3 direction); //Move from a to b
    void Rotate(Vector3 direction); //Rotate on place by x euler angles
    void Jump(); //Make the actor jump
		bool isFlying(); //is the actor in the air?
}
