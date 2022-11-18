using UnityEngine;

[RequireComponent(typeof(Actor))]
public class LifeController : MonoBehaviour, IDamageable
{
    public float MaxLife => GetComponent<Actor>().ActorStats.MaxLife;

		public float CurrentLife => _currentLife;
    [SerializeField] private float _currentLife;

    private void Start()
    {
        _currentLife = MaxLife;
        if(name == "Character")
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
    }

    public void TakeDamage(float damage)
    {
	    _currentLife -= damage;
        if (name == "Character")
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
        if (_currentLife <= 0) Die();
    }

    public void Die(){
			if(tag == "Enemy"){
				GetComponent<Animator>().Play("Die");
				//Die will be called via event once the animation completes
			} else {
				endGame();
      }
      //else{
			//	Destroy(this.gameObject);
			//}
		}

    public void Heal(float healAmmount){
			float newHealth = _currentLife + healAmmount;
			_currentLife = (newHealth > MaxLife) ? MaxLife : newHealth;
			EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
		}

    private void endGame()
    {
        EventsManager.instance.EventGameOver(false);
    }
}
