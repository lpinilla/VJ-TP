using UnityEngine;

public class EnemyMutant : Enemy
{

    private EnemyController _enemyController;

		[SerializeField] private Transform playerTransform;

		void Start() {
			_enemyController = GetComponent<EnemyController>();
		}

		void Update(){
			if(_enemyController.isWithinRange(playerTransform.position)) _enemyController.Follow(playerTransform.position);
		}


}
