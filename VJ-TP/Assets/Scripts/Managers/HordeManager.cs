using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class HordeManager : MonoBehaviour {

	[SerializeField] private Renderer[] hordeIndicators; //lights
	[SerializeField] private Light mainRoomLight;
	[SerializeField] private GameObject bossPrefab;

	[SerializeField] private GameObject keyPrefab;
	[SerializeField] private GameObject exitTrigger;

	[SerializeField] private Transform bossSpawnPoint;

	[SerializeField] private Transform[] possibleShortSpawnPoints;
	[SerializeField] private Transform[] possibleLongSpawnPoints;

	[SerializeField] private PostProcessVolume _postProcessVolume;


	public int CurrentHored => _currentHorde;
	private int _currentHorde;
	private Spawner<Enemy> _enemyFactory;

	public bool InHorde => _inHorde;
	private bool _inHorde;

	[SerializeField] private HordeStats _hordeStats;

	private Color _mainRoomLightStartingColor;

	[SerializeField] private int _aliveEnemyCount;

	private ColorGrading _colorGrading;
	private ColorParameter originalColorGrading;
	[SerializeField] private ColorParameter redColorGrading;
	[SerializeField] private ColorParameter blueColorGrading;
	[SerializeField] private float colorTransitionTime;


	void Start(){
		_inHorde = false;
		_currentHorde = 0;
		_mainRoomLightStartingColor = mainRoomLight.color;
		_enemyFactory = new Spawner<Enemy>();
		_aliveEnemyCount = 1;
		EventsManager.instance.EnemyDeathEvent += OneLessEnemy;
		EventsManager.instance.StartHordesEvent += StartHordes;
		_postProcessVolume.profile.TryGetSettings(out _colorGrading);
		originalColorGrading = _colorGrading.colorFilter;
	}

	void StartHordes(){
		_inHorde = true;
		_enemyFactory.CreateN(_hordeStats.EnemyPrefab, possibleShortSpawnPoints, _hordeStats.EnemiesInFirstRound);
		_aliveEnemyCount = _hordeStats.EnemiesInFirstRound;
		mainRoomLight.color = Color.red;
		_colorGrading.colorFilter.Interp(_colorGrading.colorFilter, redColorGrading, 1f);
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
				EventsManager.instance.StartBossMusic();
				_aliveEnemyCount = 1;
				
				Action spawnBoss = () => Instantiate(bossPrefab, bossSpawnPoint);
				StartCoroutine(SleepAndFunc(6, spawnBoss)); 
				
				// Instantiate(bossPrefab, bossSpawnPoint);
				RoundIndicator();
				break;
			case 1:
				//spawn second round of enemies
				_enemyFactory.CreateN(_hordeStats.EnemyPrefab, possibleLongSpawnPoints, _hordeStats.EnemiesInSecondRound); 
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
		_colorGrading.colorFilter.Interp(_colorGrading.colorFilter, blueColorGrading, 1);
		//spawn key
		Vector3 keySpawn = bossSpawnPoint.position + new Vector3(0, 1f, 0);
		Instantiate(keyPrefab, keySpawn, Quaternion.identity);
		//enable trigger
		exitTrigger.gameObject.SetActive(true);
	}

	void RoundIndicator(){
		hordeIndicators[_currentHorde].material.SetColor("_EmissionColor", Color.red);
		//also add sound
	}
	
	IEnumerator SleepAndFunc(float seconds, Action func)
	{
		yield return new WaitForSeconds(seconds);
		func();
	}


}
