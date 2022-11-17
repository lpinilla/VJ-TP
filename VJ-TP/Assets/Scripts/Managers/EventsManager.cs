using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    static public EventsManager instance;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Enemy> _enemyInstances = new List<Enemy>();
    [SerializeField] private List<Transform> _spawnParentList;

    
    private Spawner<Enemy> _enemyFactory = new Spawner<Enemy>();
    public List<Enemy> EnemyInstances => _enemyInstances;
    
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

		//Player Interaction Events
		//public delegate void ClickAction();
		public event Action OnToggleScope;

		public void ScopeToggle(){
			if (OnToggleScope != null) OnToggleScope();
		}

    public event Action<bool> OnGameOver;

    public void EventGameOver(bool isVictory) {
        if (OnGameOver != null) OnGameOver(isVictory);
    }

    public event Action<int> OnWeaponChange;
    public event Action<float, float> OnCharacterLifeChange;

    public void CharacterLifeChange(float currentLife, float maxLife) {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }

    public void WeaponChange(int weaponId) {
        if (OnWeaponChange != null) OnWeaponChange(weaponId);
    }

	public event Action StartIntroCutscene;
	public event Action FinishIntroCutscene;

	public void IntroCutscene(){
		if(StartIntroCutscene != null) StartIntroCutscene();
	}

	public void StopIntroCutscene(){
		if(FinishIntroCutscene != null) FinishIntroCutscene();
	}

	public void startRound()
	{
		foreach (var spawn in _spawnParentList)
		{
			for (int i = 0; i < 10; i++)
			{
				var enemy = _enemyFactory.Create(_enemyPrefab, spawn);
				_enemyInstances.Add(enemy);
			}
		}
	}

	public void monsterDeath(Enemy deadEnemy)
	{
		if (_enemyInstances.Count == 1)
		{
			startRound();
		}
		_enemyInstances.Remove(deadEnemy);
		Destroy(deadEnemy.gameObject);
		
	}
}
