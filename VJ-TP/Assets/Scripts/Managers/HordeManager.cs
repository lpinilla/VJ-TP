using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HordeManager : MonoBehaviour {

	[SerializeField] private Renderer[] hordeIndicators; //lights
	[SerializeField] private Light mainRoomLight;
	[SerializeField] private GameObject bossPrefab;

	[SerializeField] private GameObject keyPrefab;
	[SerializeField] private GameObject exitTrigger;

	[SerializeField] private Transform bossSpawnPoint;
	[SerializeField] private Transform[] possibleSpawnPoints;

	public int CurrentHored => _currentHorde;
	private int _currentHorde;
	private Spawner<Enemy> _enemyFactory;

	public bool InHorde => _inHorde;
	private bool _inHorde;

	[SerializeField] private HordeStats _hordeStats;

	private Color _mainRoomLightStartingColor;

	[SerializeField] private int _aliveEnemyCount;

	void Start(){
		_inHorde = false;
		_currentHorde = 0;
		_mainRoomLightStartingColor = mainRoomLight.color;
		_enemyFactory = new Spawner<Enemy>();
		_aliveEnemyCount = 0;
		EventsManager.instance.EnemyDeathEvent += OneLessEnemy;
		EventsManager.instance.StartHordesEvent += StartHordes;

	}

	void StartHordes(){
		_inHorde = true;
		_enemyFactory.CreateN(_hordeStats.EnemyPrefab, possibleSpawnPoints, _hordeStats.EnemiesInFirstRound);
		_aliveEnemyCount = _hordeStats.EnemiesInFirstRound;
		mainRoomLight.color = Color.red;
		RoundIndicator();
	}

	//called every time every enemy from this round is killed
	void FinishRound(){
		_currentHorde++;
		
		switch(_currentHorde){
			case 3:
				//boss killed, should drop key
				FinishHordes();
				break;
			case 2:
				_aliveEnemyCount = 1;
				Instantiate(bossPrefab, bossSpawnPoint);
				RoundIndicator();
				break;
			case 1:
				//spawn second round of enemies
				_enemyFactory.CreateN(_hordeStats.EnemyPrefab, possibleSpawnPoints, _hordeStats.EnemiesInSecondRound); 
				_aliveEnemyCount = _hordeStats.EnemiesInSecondRound;
				RoundIndicator();
				break;
			default:
				break;
		}
	}

	void OneLessEnemy(){
		_aliveEnemyCount--;
		if(_aliveEnemyCount == 0) FinishRound();
	}


	void FinishHordes(){
		//renew lights
		mainRoomLight.color = _mainRoomLightStartingColor;
		for(int i = 0 ; i < hordeIndicators.Length; i++){
			hordeIndicators[i].material.SetColor("_EmissionColor", Color.green);
		}
		Vector3 keySpawn = bossSpawnPoint.position + new Vector3(0, 1f, 0);
		Instantiate(keyPrefab, keySpawn, Quaternion.identity);
		//enable trigger
		exitTrigger.gameObject.SetActive(true);
	}

	void RoundIndicator(){
		hordeIndicators[_currentHorde].material.SetColor("_EmissionColor", Color.red);
		//also add sound
	}


}
