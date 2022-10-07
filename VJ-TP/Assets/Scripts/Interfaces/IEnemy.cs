using UnityEngine;

public interface IEnemy
{
		bool isWithinRange(Vector3 targetPosition); //check if target is inside detection range

    void Follow(Vector3 targetPosition); //move enemy to position

    void StopFollowing(); //stop following if the target is outside range, maybe random/idle walk?

    void Attack(); //attack
}
