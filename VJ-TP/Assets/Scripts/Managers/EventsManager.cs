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
		public event Action onToggleScope;

		public void ScopeToggle(){
			if (onToggleScope != null) onToggleScope();
		}

    #region GAME_OVER
    public event Action<bool> OnGameOver;

    public void EventGameOver(bool isVictory) {
        if (OnGameOver != null) OnGameOver(isVictory);
    }
    #endregion

    //#region UI_EVENTS
    //public event Action<int, int> OnAmmoChange;
    public event Action<int> OnWeaponChange;
    public event Action<float, float> OnCharacterLifeChange;
    public event Action<int> OnAvatarChange;

    //public void AmmoChange(int currentAmmo, int maxAmmo) {
    //    if (OnAmmoChange != null) OnAmmoChange(currentAmmo, maxAmmo);
    //}

    public void CharacterLifeChange(float currentLife, float maxLife) {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }

    public void WeaponChange(int weaponId) {
        if (OnWeaponChange != null) OnWeaponChange(weaponId);
    }

    public void AvatarChange(int avatarId) {
        if (OnAvatarChange != null) OnAvatarChange(avatarId);
    }
    //#endregion
		//

}
