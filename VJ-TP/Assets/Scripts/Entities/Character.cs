using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Actor
{
	public Animator TunnelEntrance;
	// Controllers
    private MovementController _movementController;
	private Animator _animatorController;
    private InteractableController _interactableController;
	private LifeController _lifeController;

	private Renderer lifeBarRenderer;

	public bool HasKey => _hasKey;
	private bool _hasKey;


    [SerializeField] private List<BaseGun> _guns;
    private BaseGun _currentGun;


		// Camera bindings
		[SerializeField] private string xAxis = "Mouse X";
		[SerializeField] private string yAxis = "Mouse Y";

    // Movement Bindings
    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBack = KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _interact = KeyCode.E;
    //[SerializeField] private KeyCode _sprint = KeyCode.LeftShift;

    // Combat Bindings
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _scope = KeyCode.Mouse1;
    [SerializeField] private KeyCode _reload = KeyCode.R;

    [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _weaponSlot3 = KeyCode.Alpha3;

		//Pause
    [SerializeField] private KeyCode _pause = KeyCode.P;

    [SerializeField] private KeyCode _setVictory = KeyCode.Return;
    [SerializeField] private KeyCode _setDefeat = KeyCode.Backspace;

    /* Commands */
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBack;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;
	private CmdRotation _cmdRotation;
    private CmdJump _cmdJump;
    private CmdAttack _cmdAttack;
    private CmdInteract _cmdInteract;

	private float rotationY;
	private float rotationX;
	private bool _areControllersFrozen;

	private float shotCounter = 0;
	private bool _isFiring = false;

	private void Start()
    {
        _movementController = GetComponent<MovementController>();
		_animatorController = GetComponentInChildren(typeof(Animator)) as Animator;
		_lifeController = GetComponent<LifeController>();
		lifeBarRenderer = transform.Find("Model/root/pelvis/spine_01/spine_02/spine_03/clavicle_l/upperarm_l/lowerarm_l/lowerarm_twist_01_l/HealthIndicator").GetComponent<MeshRenderer>();
        ChangeWeapon(0);
        _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMoveBack	= new CmdMovement(_movementController, Vector3.back);
        _cmdMoveLeft	= new CmdMovement(_movementController, Vector3.left);
        _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
        _cmdRotation = new CmdRotation(_movementController, Vector3.zero);
        _cmdJump = new CmdJump(_movementController);
		_cmdAttack = new CmdAttack(_guns[0]);
		//_cmdInteract = object the player is looking at
		Cursor.lockState = CursorLockMode.Locked;
		EventsManager.instance.OnCharacterLifeChange += ChangeHealthIndicator;
		_hasKey = false;
		EventsManager.instance.StartIntroCutscene += FreezeControllers;
		EventsManager.instance.FinishIntroCutscene += UnfreezeControllers;
		EventsManager.instance.Level1FinaleEvent += LoadTunnel;
    }

    void Update() {

				if(Input.GetKey(_pause)) Debug.Break();

		//Calculate player's rotation
		rotationY += Input.GetAxis(xAxis);
		rotationX -= Input.GetAxis(yAxis);
		_cmdRotation = new CmdRotation(_movementController, new Vector3(0, rotationY, 0));
		EventQueueManager.instance.AddMovementCommand(_cmdRotation);


		//Add movement commands to queue
        if (Input.GetKey(_moveForward) && !_areControllersFrozen){
			EventQueueManager.instance.AddMovementCommand(_cmdMoveForward);
			ChangeAnimation("Rifle Walk");
		}
        if (Input.GetKey(_moveBack) && !_areControllersFrozen){
			EventQueueManager.instance.AddMovementCommand(_cmdMoveBack);
			ChangeAnimation("Rifle Walk");
		}
        if (Input.GetKey(_moveLeft) && !_areControllersFrozen){
			EventQueueManager.instance.AddMovementCommand(_cmdMoveLeft);
			ChangeAnimation("Rifle Walk");
		}
        if (Input.GetKey(_moveRight) && !_areControllersFrozen){
			EventQueueManager.instance.AddMovementCommand(_cmdMoveRight);
			ChangeAnimation("Rifle Walk");
		}
        if (!_movementController.isFlying() && Input.GetKey(_jump) && !_areControllersFrozen) EventQueueManager.instance.AddMovementCommand(_cmdJump);
			//Add interact command to queue
			//if (Input.GetKey(_interact))	EventQueueManager.instance.AddCommand(_cmdInteract);
        if (Input.GetKeyDown(_reload) && !_areControllersFrozen){
			//play animation
			ChangeAnimation("Reload");
			//reload gun bullets
			_currentGun?.Reload();
		}
        if (Input.GetKey(_attack) && !_areControllersFrozen) EventQueueManager.instance.AddCommand(_cmdAttack);
        if (Input.GetKeyDown(_scope) && !_areControllersFrozen) EventsManager.instance.ScopeToggle();
        if (Input.GetKeyDown(_weaponSlot1) && !_areControllersFrozen) ChangeWeapon(0);
        //if (Input.GetKeyDown(_weaponSlot2) && !_areControllersFrozen) ChangeWeapon(1);
        //if (Input.GetKeyDown(_weaponSlot3) && !_areControllersFrozen) ChangeWeapon(2);

        //TODO: REMOVE THIS KEYBINDS, ONLY FOR TESTING
        //if (Input.GetKeyDown(_setVictory)) EventsManager.instance.EventGameOver(true);
        //if (Input.GetKeyDown(_setDefeat)) _lifeController.TakeDamage(_lifeController.MaxLife);

        //failsafe, kill player if it drops below -50 on Y position
        if(transform.position.y < -50) _lifeController.TakeDamage(_lifeController.MaxLife);

    }

    private void ChangeWeapon(int index) {
        foreach (var gun in _guns) gun.gameObject.SetActive(false);
        _currentGun = _guns[index];
        _currentGun.gameObject.SetActive(true);
        //_currentGun.Reload(); keep the bullets you had
        _cmdAttack = new CmdAttack(_currentGun);
        EventsManager.instance.WeaponChange(index);
    }

		private void ChangeAnimation(string targetAnimation){
			_animatorController.Play(targetAnimation);
		}

		void OnTriggerEnter(Collider other){
			if(other.tag == "EnemyDamage"){
				Debug.Log("hit by moster");
				if((other.GetComponentInParent(typeof(EnemyMutant)) as EnemyMutant).IsAttacking){
					Debug.Log("Was in attack mode");
					_lifeController.TakeDamage(40);
				}else{
					Debug.Log("Was not in attack mode");
				} 
			}else if(other.tag == "Health"){
				if(_lifeController.CurrentLife != _lifeController.MaxLife){
					ICurable c = (other.GetComponentInParent(typeof(HealthPack)) as HealthPack);
					_lifeController.Heal(c.HealAmmount);
					Destroy(other.gameObject);
				}
			} else if(other.tag == "Cutscene"){
				EventsManager.instance.IntroCutscene();
				Destroy(other.gameObject);
			} else if(other.tag == "Key"){
				_hasKey = true;
				Destroy(other.gameObject);
				EventsManager.instance.PlayScapeVoiceLine();
			} else if(other.tag == "NextLevel"){
				if(_hasKey){
					EventsManager.instance.EndLevel1();
					Destroy(other.gameObject);
				}
			} else if(other.tag == "TunnelEntrance"){
				TunnelEntrance.SetBool("isOpen",false);
				Destroy(other.gameObject);
			} else if(other.tag == "TunnelExit"){
				EventsManager.instance.StartLevel2();
				Destroy(other.gameObject);
		}
		}

		public void ChangeHealthIndicator(float currHealth, float MaxHealth){
			float healthPercentage = 1f - currHealth / MaxHealth;
			Color currentColor = Color.Lerp(Color.green, Color.red, healthPercentage);
			lifeBarRenderer.material.SetColor("_EmissionColor", currentColor);
		}

		void FreezeControllers(){
			_areControllersFrozen = true;
		}

		void UnfreezeControllers(){
			_areControllersFrozen = false;
		}

		void LoadTunnel()
		{
			StartCoroutine(LoadAsync("Tunnel"));
			TunnelEntrance.SetBool("isOpen",true);
		}

		
		
		IEnumerator LoadAsync(String name)
		{
			AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
			operation.allowSceneActivation = false;

			while (!operation.isDone)
			{

				if(operation.progress >= .9f) operation.allowSceneActivation = true;
            
				yield return null;
			}
		}

}
