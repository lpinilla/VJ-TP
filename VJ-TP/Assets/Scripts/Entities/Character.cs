using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    // Instances
    private MovementController _movementController;
    private InteractableController _interactableController;
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
    [SerializeField] private KeyCode _reload = KeyCode.R;

    [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _weaponSlot3 = KeyCode.Alpha3;

    [SerializeField] private KeyCode _setVictory = KeyCode.Return;
    [SerializeField] private KeyCode _setDefeat = KeyCode.Backspace;

    //[SerializeField] private float _timer = 3;

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


    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        ChangeWeapon(0);
        _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMoveBack	= new CmdMovement(_movementController, Vector3.back);
        _cmdMoveLeft	= new CmdMovement(_movementController, Vector3.left);
        _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
        _cmdRotation = new CmdRotation(_movementController, new Vector3(0, -90f, 0));
        _cmdJump = new CmdJump(_movementController);
				_cmdAttack = new CmdAttack(_guns[0]);
				//_cmdInteract = object the player is looking at
    }

    void Update() {

				//Calculate player's rotation
				rotationY += Input.GetAxis(xAxis);
				rotationX -= Input.GetAxis(yAxis);
				_cmdRotation = new CmdRotation(_movementController, new Vector3(0, rotationY, -rotationX));
				EventQueueManager.instance.AddMovementCommand(_cmdRotation);


				//Add movement commands to queue
        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddMovementCommand(_cmdMoveForward);
        if (Input.GetKey(_moveBack))    EventQueueManager.instance.AddMovementCommand(_cmdMoveBack);
        if (Input.GetKey(_moveLeft))    EventQueueManager.instance.AddMovementCommand(_cmdMoveLeft);
        if (Input.GetKey(_moveRight))   EventQueueManager.instance.AddMovementCommand(_cmdMoveRight);
        if (!_movementController.isFlying() && Input.GetKey(_jump)) EventQueueManager.instance.AddMovementCommand(_cmdJump);

				//Add interact command to queue
				if (Input.GetKey(_interact))		EventQueueManager.instance.AddCommand(_cmdInteract);

				//Add combat commands to queue
        if (Input.GetKeyDown(_reload)) _currentGun?.Reload();
        if (Input.GetKeyDown(_attack)) EventQueueManager.instance.AddCommand(_cmdAttack);
        if (Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
        if (Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);
        if (Input.GetKeyDown(_weaponSlot3)) ChangeWeapon(2);

				//TODO: REMOVE THIS KEYBINDS, ONLY FOR TESTING
        if (Input.GetKeyDown(_setVictory)) EventsManager.instance.EventGameOver(true);
        if (Input.GetKeyDown(_setDefeat)) GetComponent<IDamageable>().TakeDamage(20);

				//failsafe, kill player if it drops below -50 on Y position
				if(transform.position.y < -50) GetComponent<IDamageable>().TakeDamage(20);

    }

    private void ChangeWeapon(int index) {
        foreach (var gun in _guns) gun.gameObject.SetActive(false);
        _currentGun = _guns[index];
        _currentGun.gameObject.SetActive(true);
        //_currentGun.Reload(); keep the bullets you had
        _cmdAttack = new CmdAttack(_currentGun);
        EventsManager.instance.WeaponChange(index);
    }

}
