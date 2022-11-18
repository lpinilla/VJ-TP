using UnityEngine;

public class EnemyMutant : Enemy
{

    private EnemyController _enemyController;
		private Animator _enemyAnimator;

		private bool _taunting;
		private bool _taunted;
		private bool _wasInRange;

		public bool IsAttacking => _isAttacking;
		private bool _isAttacking;
		private bool _isDead;

		private Transform playerTransform;

		void Start() {
			playerTransform = GameObject.FindWithTag("Character").transform;
			_enemyController = GetComponent<EnemyController>();
			_enemyAnimator = GetComponent<Animator>();
			_taunted = false;
			_taunting = false;
			_wasInRange = false;
			_isDead = false;
		}

		void Update(){
			if(!_isDead){
				if(_enemyController.isWithinAttackRange(playerTransform.position)){
					_enemyController.StopFollowing();
					_taunted = false;
					//play attack animation
					ChangeAnimation("Attack");
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
		}

		public void StartAttack() => _isAttacking = true;

		public void FinishAttack() => _isAttacking = false;

		public void Taunt(){
			_taunting = false;
			_taunted = true;
			_enemyController.Follow(playerTransform.position); //force follow after taunt animation
		}

		public void AfterDeath(){
			_isDead = true;
			Debug.Log("im DEADD");
			EventsManager.instance.monsterDeath(this);
		}

		private void ChangeAnimation(string targetAnimation){
			_enemyAnimator.Play(targetAnimation);
		}

}
