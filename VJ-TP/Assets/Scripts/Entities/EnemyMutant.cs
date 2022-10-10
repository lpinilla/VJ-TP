using UnityEngine;

public class EnemyMutant : Enemy
{

    private EnemyController _enemyController;
		private Animator _enemyAnimator;

		private bool _taunting;
		private bool _taunted;
		private bool _wasInRange;

		[SerializeField] private Transform playerTransform;

		void Start() {
			_enemyController = GetComponent<EnemyController>();
			_enemyAnimator = GetComponent<Animator>();
			_taunted = false;
			_taunting = false;
			_wasInRange = false;
		}

		void Update(){
			if(_enemyController.isWithinAttackRange(playerTransform.position)){
				_taunted = false;
				//play attack animation
				ChangeAnimation("Die"); //TODO test remove
				//atack
			}else if(_enemyController.isWithinDetectionRange(playerTransform.position)){
					//play animation
					if(!_taunted){
						if(!_taunting){
							_taunting = true;
							ChangeAnimation("Taunt");
						}
					}else{
						_enemyController.Follow(playerTransform.position);
					}
					_wasInRange = true;
			}else{
				_enemyController.StopFollowing();
				if (_wasInRange){
					ChangeAnimation("Stop Running");
					_wasInRange = false; //has no new target
				}
				_taunting = false;
				_taunted	= false;
			}
		}

		public void Taunt(){
			_taunting = false;
			_taunted = true;
			_enemyController.Follow(playerTransform.position); //force follow after taunt animation
		}

		private void ChangeAnimation(string targetAnimation){
			_enemyAnimator.Play(targetAnimation);
		}



}