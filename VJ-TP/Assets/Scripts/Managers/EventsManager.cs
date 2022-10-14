using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    static public EventsManager instance;

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


}
