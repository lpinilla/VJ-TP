using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    static public EventsManager instance;
    [SerializeField] private Enemy _enemyPrefab;
    
    [SerializeField] private int[] _rounds;
    [SerializeField] private int _currentRound;
    [SerializeField] private List<Transform> _spawnParentList;
    [SerializeField] private float _roundPointsValue;
    public event Action OnToggleScope;
    public event Action<bool> OnGameOver;
    public event Action<int> OnWeaponChange;
    public event Action<float, float> OnCharacterLifeChange;
    public event Action StartIntroCutscene;
    public event Action FinishIntroCutscene;
    public event Action StartHordesEvent;
    public event Action Level2Event;
    public event Action Level1FinaleEvent;
    public event Action EnemyDeathEvent;
    public event Action SoundStartEvent;
    public event Action StartBossMusicEvent;
    public event Action ScapeVoicelineEvent;


    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

	//Player Interaction Events
	//public delegate void ClickAction();
	public void ScopeToggle(){
		if (OnToggleScope != null) OnToggleScope();
	} 
	public void EventGameOver(bool isVictory) {
        if (OnGameOver != null) OnGameOver(isVictory);
    }
	public void CharacterLifeChange(float currentLife, float maxLife) {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    } 
	public void WeaponChange(int weaponId) {
        if (OnWeaponChange != null) OnWeaponChange(weaponId);
    }
	public void IntroCutscene(){
		if(StartIntroCutscene != null) StartIntroCutscene();
	}
	//finish cutscene and start hordes
	public void StopIntroCutscene(){
		if(FinishIntroCutscene != null){
			FinishIntroCutscene();
			StartHordesEvent();
			SoundStartEvent();
		}
	}
	public void EnemyDeath(){
		if(EnemyDeathEvent != null) EnemyDeathEvent();
	}
	public void EndLevel1()
	{
		Level1FinaleEvent();
	}
	public void StartLevel2()
	{
		Level2Event();
	}
	public void StartBossMusic()
	{
		StartBossMusicEvent();
	}

	public void PlayScapeVoiceLine()
	{
		ScapeVoicelineEvent();
	}
}
